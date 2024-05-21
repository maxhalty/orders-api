
namespace OrderAPI.Domain.Tests;

public class OrderTests
{
    private readonly MockRepository _mockRepository;

    public OrderTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
    }

    [Fact]
    public void CreateOrder_CorrectData_OrderOk()
    {
        string expectedName = "NewOrder";
        decimal expectedPrice = 100;
        Order order = new(expectedName, It.IsAny<string>(), expectedPrice);

        Assert.NotNull(order);
        Assert.Equal(expectedName, order.Name);
        Assert.Equal(expectedPrice, order.Price);
        Assert.NotEqual(Guid.Empty, order.Id);
    }
}
