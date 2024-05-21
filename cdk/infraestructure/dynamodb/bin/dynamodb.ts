#!/usr/bin/env node
import 'source-map-support/register';
import * as cdk from 'aws-cdk-lib';
import { DynamodbStack } from '../lib/dynamodb-stack';
import { TagsHelpers } from './helpers/tag-helpers';

if (process.env.AWS_DEPLOY_REGION === undefined) {
  throw new Error("AWS_DEPLOY_REGION environment variable missing");
}

if (process.env.AWS_DR_REGION === undefined) {
  throw new Error("AWS_DR_REGION environment variable missing");
}

const app = new cdk.App();

const _dynamodbStack = new DynamodbStack(app, 'orders-dynamodb-stack');

TagsHelpers.addTags(_dynamodbStack);