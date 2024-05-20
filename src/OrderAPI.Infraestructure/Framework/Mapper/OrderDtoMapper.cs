using OrderAPI.Domain;
using OrderAPI.Infraestructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
