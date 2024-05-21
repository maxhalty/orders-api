using OrderAPI.Application.Queries;
using OrderAPI.Common.Framework.Handlers;
using OrderAPI.Common.Framework.Helpers;
using OrderAPI.Common.Framework.Mappers;
using OrderAPI.GetByOrderId.Queries;
using OrderAPI.Infraestructure.Framework.Helpers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddHttpContextAccessor();

builder.Services.AddCommonServices(builder.Configuration, builder.Environment);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetOrderByIdQuery).Assembly));

WebApplication app = builder.Build();

app.UseApiExceptionHandler(ExceptionsMapper.GetApiDictionary());

app.UseHttpsRedirection();
app.UseCors("OrderApiCorsPolicy");

//app.UseAuthorization();

if (!app.Environment.IsProduction())
{
    _ = app.AddSwaggerConfiguration(builder.Configuration);
}

app.AddOrdersQueries();

app.MapGet("/public/healthcheck", () => $"I'm Healthy :), hello from {EnvironmentHelper.GetEnvironmentVariable("REGION")}")
    .ExcludeFromDescription();

app.Run();
