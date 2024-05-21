#!/usr/bin/env node
import 'source-map-support/register';
import * as cdk from 'aws-cdk-lib';
import { GetByOrderIdStack } from '../lib/get-by-order-id-stack';
import { TagsHelpers } from './helpers/tag-helpers';

if (process.env.AWS_DEPLOY_REGION === undefined) {
  throw new Error("AWS_DEPLOY_REGION environment variable missing");
}

const app = new cdk.App();

const _getByOrderIdStack = new GetByOrderIdStack(app, 'get-by-order-id-stack');

TagsHelpers.addTags(_getByOrderIdStack);