using MediatR;
using Microsoft.AspNetCore.Http;

namespace OrderAPI.Application.Commands;

public sealed class CreateOrderCommand(string orderName, string orderDescription, decimal orderPrice) : IRequest<IResult>
{
    public string OrderName { get; set; } = orderName;
    public string OrderDescription { get; set; } = orderDescription;
    public decimal OrderPrice { get; set; } = orderPrice;
}
