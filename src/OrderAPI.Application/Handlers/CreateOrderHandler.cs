using Amazon.DynamoDBv2.DataModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using OrderAPI.Application.Commands;
using OrderAPI.Application.Extensions;
using OrderAPI.Domain;
using OrderAPI.Infraestructure.Repositories;

namespace OrderAPI.Application.Handlers;

public sealed class CreateOrderHandler : IRequestHandler<CreateOrderCommand, IResult>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDynamoDBContext? _dynamoDBContext;

    public CreateOrderHandler(IHttpContextAccessor httpContextAccessor, IDynamoDBContext? dynamoDBContext = null)
    {
        _httpContextAccessor = httpContextAccessor;
        _dynamoDBContext = dynamoDBContext;
    }

    public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = new(
            request.OrderName,
            request.OrderDescription,
            request.OrderPrice);

        _ = await order.SaveAsync(_dynamoDBContext);

        return Results.Created(_httpContextAccessor.HttpContext?.Request.GetUriByPath($"/v1/{order.Id}")!, null);
    }
}
