import { Stack, CfnOutput } from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as apigateway from 'aws-cdk-lib/aws-apigatewayv2';
import { HttpLambdaIntegration } from 'aws-cdk-lib/aws-apigatewayv2-integrations';
import { Function } from 'aws-cdk-lib/aws-lambda';

export class ApiGatewayStack extends Stack {
  constructor(scope: Construct, id: string) {
    super(scope, id);

    if (process.env.CORS_ALLOW_ORIGINS === undefined) {
      throw new Error("CORS_ALLOW_ORIGINS environment variable missing");
    }
    
    if (process.env.CORS_EXPOSE_HEADERS === undefined) {
      throw new Error("CORS_EXPOSE_HEADERS environment variable missing");
    }

    const httpApi = new apigateway.HttpApi(this, `orders-api-${process.env.AWS_DEPLOY_REGION}`, {
      createDefaultStage: true,
      corsPreflight: {
        allowHeaders: ['*'],
        allowMethods: [apigateway.CorsHttpMethod.ANY],
        allowOrigins: process.env.CORS_ALLOW_ORIGINS.split(","),
        exposeHeaders: process.env.CORS_EXPOSE_HEADERS.split(",")
      },
      apiName: `orders-api-${process.env.AWS_DEPLOY_REGION}`
    });

    new CfnOutput(this, `output-order-api-id-${process.env.AWS_DEPLOY_REGION}`, {
      exportName: `output-order-api-id-${process.env.AWS_DEPLOY_REGION}`,
      value: httpApi.apiId
    });

    const defaultStage = httpApi.defaultStage?.node.defaultChild as apigateway.CfnStage;
    defaultStage.defaultRouteSettings = {
      throttlingBurstLimit: 5000,
      throttlingRateLimit: 10000
    };

    //=======================================================
    // GET BY ORDER ID 
    //=======================================================

    let getByOrderIdFunctionName = `get-order-by-id-${process.env.AWS_DEPLOY_REGION}`;

    const getByOrderIdFunction = Function.fromFunctionName(
      this,
      getByOrderIdFunctionName,
      getByOrderIdFunctionName
    );

    const getByOrderIdFunctionIntegration = new HttpLambdaIntegration(
      `get-order-by-id-integration-${process.env.AWS_DEPLOY_REGION}`,
      getByOrderIdFunction
    );

    httpApi.addRoutes({
      path: '/v1/{orderId}',
      methods: [apigateway.HttpMethod.GET],
      integration: getByOrderIdFunctionIntegration
    });

    //=======================================================
    // POST ORDER 
    //=======================================================

    let postFunctionName = `post-order-${process.env.AWS_DEPLOY_REGION}`;

    const postFunction = Function.fromFunctionName(
      this,
      postFunctionName,
      postFunctionName
    );

    const postFunctionIntegration = new HttpLambdaIntegration(
      `post-order-integration-${process.env.AWS_DEPLOY_REGION}`,
      postFunction
    );

    httpApi.addRoutes({
      path: '/v1',
      methods: [apigateway.HttpMethod.POST],
      integration: postFunctionIntegration
    });

    httpApi.addRoutes({
      path: '/public/healthcheck',
      methods: [apigateway.HttpMethod.GET],
      integration: postFunctionIntegration
    });

    httpApi.addRoutes({
      path: '/public/swagger/{proxy+}',
      methods: [apigateway.HttpMethod.GET],
      integration: postFunctionIntegration
    });
  }
}
