using Amazon.DynamoDBv2.DataModel;
using OrderAPI.Domain;
using OrderAPI.Infraestructure.DbContexts;
using OrderAPI.Infraestructure.Framework.Mapper;
using System.Diagnostics.CodeAnalysis;

namespace OrderAPI.Infraestructure.Repositories;

[ExcludeFromCodeCoverage]
public static class WriteRepository
{
    private static IDynamoDBContext _dbContext = OrdersDbContext.DbContext;

    public static async Task<Order> SaveAsync(this Order order, IDynamoDBContext? dbContext = null)
    {
        ValidateDbContext(dbContext);

        await _dbContext.SaveAsync(order.ToDto(), OrdersDbContext.OperationConfig);

        return order;
    }

    private static void ValidateDbContext(IDynamoDBContext? dbContext)
    {
        if (dbContext is not null)
        {
            _dbContext = dbContext;
        }
    }
}
