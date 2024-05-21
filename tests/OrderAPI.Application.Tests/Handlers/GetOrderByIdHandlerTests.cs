
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrderAPI.Application.Handlers;
using OrderAPI.Application.Queries;
using OrderAPI.Infraestructure.Dtos;
using OrderAPI.Infraestructure.Framework.Exceptions;

namespace OrderAPI.Application.Tests.Handlers;

public class GetOrderByIdHandlerTests
{
    private readonly MockRepository _mockRepository;
    private readonly Mock<IDynamoDBContext> _mockDynamoDBContext;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    public GetOrderByIdHandlerTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);

        _mockDynamoDBContext = _mockRepository.Create<IDynamoDBContext>();
        _mockHttpContextAccessor = _mockRepository.Create<IHttpContextAccessor>();

        DefaultHttpContext context = new()
        {
            RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider()
        };
        _ = _mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

        Environment.SetEnvironmentVariable("DYNAMODB_ORDERS_TABLE_NAME", "orders-table");
        Environment.SetEnvironmentVariable("AWS_REGION", "us-east-1");
    }

    private GetOrderByIdHandler CreateGetOrderByIdHandler()
    {
        return new GetOrderByIdHandler(_mockDynamoDBContext.Object);
    }

    [Fact]
    public async Task Handle_CorrectData_GetAndResponseOrderById()
    {
        _ = _mockDynamoDBContext.Setup(x => x.LoadAsync<OrderDto>(
            It.IsAny<Guid>(),
            It.IsAny<DynamoDBOperationConfig>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new OrderDto() { Name = "OrderTest" });

        GetOrderByIdHandler getOrderByIdHandler = CreateGetOrderByIdHandler();
        GetOrderByIdQuery request = new(It.IsAny<Guid>());
        int expectedHttpStatusCode = 200;

        IResult result = await getOrderByIdHandler.Handle(request, It.IsAny<CancellationToken>());
        await result.ExecuteAsync(_mockHttpContextAccessor.Object.HttpContext!);

        Assert.NotNull(result);
        Assert.Equal(expectedHttpStatusCode, _mockHttpContextAccessor.Object.HttpContext!.Response.StatusCode);
    }

    [Fact]
    public async Task Handle_InvalidOrderId_ThrowOrderNotFoundException()
    {
        OrderDto? orderDto = null;
        _ = _mockDynamoDBContext.Setup(x => x.LoadAsync<OrderDto>(
            It.IsAny<Guid>(),
            It.IsAny<DynamoDBOperationConfig>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(orderDto)!);

        GetOrderByIdHandler getOrderByIdHandler = CreateGetOrderByIdHandler();
        GetOrderByIdQuery request = new(It.IsAny<Guid>());

        _ = await Assert.ThrowsAsync<OrderNotFoundException>(
            () => getOrderByIdHandler.Handle(request, It.IsAny<CancellationToken>()));
    }
}
