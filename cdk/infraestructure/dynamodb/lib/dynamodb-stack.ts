import { RemovalPolicy, Stack } from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as dynamodb from 'aws-cdk-lib/aws-dynamodb';

export class DynamodbStack extends Stack {
  public readonly DynamoDbTableResult: dynamodb.Table;

  constructor(scope: Construct, id: string) {
    super(scope, id);

    let replicationRegions: string[] = [];
    let removalPolicy: RemovalPolicy = RemovalPolicy.DESTROY;

    if (process.env.AWS_DR_REGION) {
      replicationRegions.push(process.env.AWS_DR_REGION);
    }

    const tableName = "orders-table";

    this.DynamoDbTableResult = new dynamodb.Table(this, tableName, {
      tableName: tableName,
      partitionKey: {
        name: 'Id',
        type: dynamodb.AttributeType.STRING
      },
      billingMode: dynamodb.BillingMode.PAY_PER_REQUEST,
      encryption: dynamodb.TableEncryption.AWS_MANAGED,
      removalPolicy: removalPolicy,
      replicationRegions: replicationRegions,
      deletionProtection: false
    });
  }
}
