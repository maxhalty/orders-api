using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using OrderAPI.Infraestructure.Framework.Helpers;

namespace OrderAPI.Common.Framework.Helpers;

[ExcludeFromCodeCoverage]
public static class CorsHelper
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        _ = services.AddCors(o => o.AddPolicy("OrderApiCorsPolicy", builder =>
        {
            _ = builder.WithOrigins(EnvironmentHelper.GetEnvironmentVariable("CORS_ALLOW_ORIGINS")!.Split(","))
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders(EnvironmentHelper.GetEnvironmentVariable("CORS_ALLOW_ORIGINS")!.Split(","));
        }));

        return services;
    }
}
