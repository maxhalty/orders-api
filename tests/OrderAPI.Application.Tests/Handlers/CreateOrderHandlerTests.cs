using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrderAPI.Application.Commands;
using OrderAPI.Application.Handlers;
using OrderAPI.Infraestructure.Dtos;

namespace OrderAPI.Application.Tests.Handlers;

public class CreateOrderHandlerTests
{
    private readonly MockRepository _mockRepository;
    private readonly Mock<IDynamoDBContext> _mockDynamoDBContext;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    public CreateOrderHandlerTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);

        _mockDynamoDBContext = _mockRepository.Create<IDynamoDBContext>();
        _mockHttpContextAccessor = _mockRepository.Create<IHttpContextAccessor>();

        DefaultHttpContext context = new()
        {
            RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider()
        };
        context.Request.Host = new("test.uy");
        context.Request.Scheme = "http";
        _ = _mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

        _ = _mockDynamoDBContext.Setup(x => x.SaveAsync(It.IsAny<OrderDto>(), It.IsAny<DynamoDBOperationConfig>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        Environment.SetEnvironmentVariable("DYNAMODB_ORDERS_TABLE_NAME", "orders-table");
        Environment.SetEnvironmentVariable("AWS_REGION", "us-east-1");
    }

    private CreateOrderHandler CreateCreateOrderHandler()
    {
        return new CreateOrderHandler(_mockHttpContextAccessor.Object, _mockDynamoDBContext.Object);
    }

    [Fact]
    public async Task Handle_CorrectData_CreateAndResponseNewOrder()
    {
        CreateOrderHandler createOrderHandler = CreateCreateOrderHandler();
        CreateOrderCommand orderRequest = new(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>());

        int expectedHttpStatusCode = 201;
        string expectedLocationStart = $"{_mockHttpContextAccessor.Object.HttpContext?.Request.Scheme}://" +
            $"{_mockHttpContextAccessor.Object.HttpContext?.Request.Host}/v1/";

        IResult result = await createOrderHandler.Handle(orderRequest, It.IsAny<CancellationToken>());
        await result.ExecuteAsync(_mockHttpContextAccessor.Object.HttpContext!);

        Assert.NotNull(result);
        Assert.Equal(expectedHttpStatusCode, _mockHttpContextAccessor.Object.HttpContext!.Response.StatusCode);
        Assert.StartsWith(expectedLocationStart, _mockHttpContextAccessor.Object.HttpContext!.Response.Headers.Location);
    }
}
