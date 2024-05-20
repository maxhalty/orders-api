using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace OrderAPI.Common.Framework.Helpers;

[ExcludeFromCodeCoverage]
public static class ServicesHelper
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services, ConfigurationManager configManager, IWebHostEnvironment environment)
    {
        _ = services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        });

        if (!environment.IsProduction())
        {
            _ = services.ConfigureSwagger(configManager);
        }

        _ = services.AddCorsConfiguration();

        return services;
    }
}
