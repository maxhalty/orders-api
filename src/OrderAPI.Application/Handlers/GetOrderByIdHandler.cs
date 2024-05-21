

using Amazon.DynamoDBv2.DataModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using OrderAPI.Application.Queries;
using OrderAPI.Domain;
using OrderAPI.Infraestructure.Repositories;

namespace OrderAPI.Application.Handlers;

public class GetOrderByIdHandler(IDynamoDBContext? dynamoDBContext = null) : IRequestHandler<GetOrderByIdQuery, IResult>
{
    private readonly IDynamoDBContext? _dynamoDBContext = dynamoDBContext;

    public async Task<IResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        Order order = await WriteRepository.LoadByIdAsync(request.OrderId, _dynamoDBContext);

        return Results.Ok(order);
    }
}
