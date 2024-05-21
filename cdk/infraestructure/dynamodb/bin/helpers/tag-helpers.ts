import { Aspects, Tag } from "aws-cdk-lib";
import { Construct } from "constructs";
import * as GlobalTags from "../../../../tags/tags.global.json";

export class TagsHelpers {
    static addTags(resource: Construct) {
        const tagsList = GlobalTags.TagsList;

        tagsList.forEach((tag: { Key: string; Value: string }) => {
            Aspects.of(resource).add(new Tag(tag.Key, tag.Value));
        });
    }
}