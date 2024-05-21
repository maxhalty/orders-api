using MediatR;
using OrderAPI.Application.Queries;
using OrderAPI.Domain;
using System.Diagnostics.CodeAnalysis;

namespace OrderAPI.GetByOrderId.Queries;

[ExcludeFromCodeCoverage]
public static class OrderQueries
{
    public static void AddOrdersQueries(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet("v1/{orderId}", async (IMediator mediator, Guid orderId) =>
        {
            return await mediator.Send(new GetOrderByIdQuery(orderId));
        })
        .WithName(nameof(GetOrderByIdQuery))
        .WithTags(nameof(Order))
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
