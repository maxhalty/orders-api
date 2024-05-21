#!/usr/bin/env node
import 'source-map-support/register';
import * as cdk from 'aws-cdk-lib';
import { PostOrderStack } from '../lib/post-order-stack';
import { TagsHelpers } from './helpers/tag-helpers';

if (process.env.AWS_DEPLOY_REGION === undefined) {
  throw new Error("AWS_DEPLOY_REGION environment variable missing");
}

const app = new cdk.App();

const _postOrderStack = new PostOrderStack(app, 'post-order-stack');

TagsHelpers.addTags(_postOrderStack);