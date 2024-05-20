using MediatR;
using Microsoft.AspNetCore.Http;

namespace OrderAPI.Application.Commands;

public sealed class CreateOrderCommand : IRequest<IResult>
{
    public string OrderName { get; set; }
    public string OrderDescription { get; set; }
    public decimal OrderPrice { get; set; }

    public CreateOrderCommand(string orderName, string orderDescription, decimal orderPrice)
    {
        OrderName = orderName;
        OrderDescription = orderDescription;
        OrderPrice = orderPrice;
    }
}
