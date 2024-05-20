using MediatR;
using OrderAPI.Application.Commands;
using OrderAPI.Domain;
using OrderAPI.Post.Requests;
using System.Diagnostics.CodeAnalysis;

namespace OrderAPI.Post.Commands;

[ExcludeFromCodeCoverage]
public static class OrderCommands
{
    public static void AddOrderCommands(this IEndpointRouteBuilder app)
    {
        _ = app.MapPost("v1", async (IMediator mediator, CreateOrderRequest request) =>
        {
            return await mediator.Send(new CreateOrderCommand(
                request.Name,
                request.Description,
                request.Price));
        })
            .WithName(nameof(CreateOrderCommand))
            .WithTags(nameof(Order))
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status201Created);
    }
}
