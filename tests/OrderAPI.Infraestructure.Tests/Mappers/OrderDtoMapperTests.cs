

using OrderAPI.Domain;
using OrderAPI.Infraestructure.Dtos;
using OrderAPI.Infraestructure.Framework.Mapper;

namespace OrderAPI.Infraestructure.Tests.Mappers;

public class OrderDtoMapperTests
{
    private readonly MockRepository _mockRepository;

    public OrderDtoMapperTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
    }

    [Fact]
    public void ToDto_CorrectData_MappedOk()
    {
        Order order = new(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>());
        OrderDto orderDto = OrderDtoMapper.ToDto(order);

        Assert.Equal(order.Name, orderDto.Name);
        Assert.Equal(order.Description, orderDto.Description);
        Assert.Equal(order.Price, orderDto.Price);
        _mockRepository.VerifyAll();
    }
}
