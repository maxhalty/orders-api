import { Stack, Duration } from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as lambda from 'aws-cdk-lib/aws-lambda';
import * as iam from 'aws-cdk-lib/aws-iam';
import * as policyJson from './roles/get-by-order-id-role-policies.json';

export class GetByOrderIdStack extends Stack {
  constructor(scope: Construct, id: string) {
    super(scope, id);

    if (process.env.AWS_DEPLOY_REGION === undefined) {
      throw new Error("AWS_DEPLOY_REGION environment variable missing");
    }
    
    if (process.env.ENV_NAME === undefined) {
      throw new Error("ENV_NAME environment variable missing");
    }

    if (process.env.CORS_ALLOW_ORIGINS === undefined) {
      throw new Error("CORS_ALLOW_ORIGINS environment variable missing");
    }

    if (process.env.CORS_EXPOSE_HEADERS === undefined) {
      throw new Error("CORS_EXPOSE_HEADERS environment variable missing");
    }

    let lambdaFunctionName = `get-order-by-id-${process.env.AWS_DEPLOY_REGION}`;
    let roleName = `get-order-by-id-role-${process.env.AWS_DEPLOY_REGION}`;

    const _role = new iam.Role(this, roleName, {
      roleName: roleName,
      assumedBy: new iam.ServicePrincipal('lambda.amazonaws.com'),
      description: `Role for lambda function: ${lambdaFunctionName}`
    });

    policyJson.Statement.forEach((statement) => {
      _role.addToPolicy(iam.PolicyStatement.fromJson(statement));
    });

    new lambda.Function(this, lambdaFunctionName, {
      functionName: lambdaFunctionName,
      runtime: lambda.Runtime.DOTNET_8,
      handler: 'OrderAPI.GetByOrderId',
      code: lambda.Code.fromAsset('../../../publish-dotnet/get-order-by-id.zip'),
      timeout: Duration.minutes(1),
      role: _role,
      memorySize: 1024,
      architecture: lambda.Architecture.ARM_64,
      tracing: lambda.Tracing.ACTIVE,
      insightsVersion: lambda.LambdaInsightsVersion.VERSION_1_0_229_0,
      environment:
      {
        ASPNETCORE_ENVIRONMENT: process.env.ENV_NAME,
        DYNAMODB_ORDERS_TABLE_NAME: 'orders-table',
        CORS_ALLOW_ORIGINS: process.env.CORS_ALLOW_ORIGINS,
        CORS_EXPOSE_HEADERS: process.env.CORS_EXPOSE_HEADERS,
        REGION: process.env.AWS_DEPLOY_REGION
      }
    })
  }
}
