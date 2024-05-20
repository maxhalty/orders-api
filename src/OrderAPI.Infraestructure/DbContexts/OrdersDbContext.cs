using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using OrderAPI.Infraestructure.Framework.Helpers;

namespace OrderAPI.Infraestructure.DbContexts;

public static class OrdersDbContext
{
    public static IDynamoDBContext DbContext
    {
        get
        {
            IAmazonDynamoDB client = new AmazonDynamoDBClient();
            return new DynamoDBContext(client, ContextConfig);
        }
    }

    public static DynamoDBOperationConfig OperationConfig => new()
    {
        IgnoreNullValues = true,
        OverrideTableName = EnvironmentHelper.GetEnvironmentVariable("DYNAMODB_ORDERS_TABLE_NAME"),
        IndexName = "Id"
    };

    private static DynamoDBContextConfig ContextConfig => new()
    {
        ConsistentRead = true,
    };
}
