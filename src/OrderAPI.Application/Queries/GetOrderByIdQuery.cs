using MediatR;
using Microsoft.AspNetCore.Http;

namespace OrderAPI.Application.Queries;

public sealed class GetOrderByIdQuery(Guid orderId) : IRequest<IResult>
{
    public Guid OrderId { get; set; } = orderId;
}
