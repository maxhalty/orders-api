using OrderAPI.Domain;
using OrderAPI.Infraestructure.Dtos;

namespace OrderAPI.Infraestructure.Framework.Mapper;

public static class OrderDtoMapper
{
    public static OrderDto ToDto(this Order order)
    {
        OrderDto orderDto = new()
        {
            Id = order.Id,
            Name = order.Name,
            Description = order.Description,
            Price = order.Price,
            CreatedAt = order.CreatedAt
        };

        return orderDto;
    }

    public static Order ToOrder(this OrderDto orderDto)
    {
        Order order = new(
            orderDto.Id,
            orderDto.Name,
            orderDto.Description,
            orderDto.Price,
            orderDto.CreatedAt);

        return order;
    }
}
