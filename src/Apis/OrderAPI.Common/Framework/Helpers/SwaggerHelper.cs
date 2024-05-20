using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace OrderAPI.Common.Framework.Helpers;

[ExcludeFromCodeCoverage]
public static class SwaggerHelper
{
    public static WebApplication AddSwaggerConfiguration(this WebApplication app, ConfigurationManager configManager)
    {
        string apiName = Assembly.GetCallingAssembly().GetName().Name ?? throw new MissingFieldException("Missing Assembly Application Name");
        string swaggerDocNameV1 = configManager.GetValue<string>("Swagger:SwaggerConfiguration:SwaggerVersions:v1:SwaggerDocName")!;
        string prefix = "public/swagger";

        _ = app.UseSwagger(options =>
        {
            options.RouteTemplate = $"{prefix}/{{documentName}}/swagger.json";
        });

        _ = app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/{prefix}/{swaggerDocNameV1}/swagger.json", $"{apiName} {swaggerDocNameV1}");
            options.RoutePrefix = $"{prefix}";
        });

        return app;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, ConfigurationManager configManager)
    {
        _ = services.AddEndpointsApiExplorer();
        _ = services.AddSwaggerGen(options => 
        {
            options.SwaggerDoc(
                configManager.GetValue<string>("Swagger:SwaggerConfiguration:SwaggerVersions:v1:SwaggerDocName"),
                new OpenApiInfo
                {
                    Title = configManager.GetValue<string>("Swagger:SwaggerConfiguration:SwaggerVersions:v1:OpenApiInfo:Title"),
                    Version = configManager.GetValue<string>("Swagger:SwaggerConfiguration:SwaggerVersions:v1:OpenApiInfo:Version"),
                    Description = configManager.GetValue<string>("Swagger:SwaggerConfiguration:OpenApiInfo:Description"),
                    Contact = new OpenApiContact
                    {
                        Name = configManager.GetValue<string>("Swagger:SwaggerConfiguration:OpenApiInfo:Contact:Name"),
                        Url = new Uri(configManager.GetValue<string>("Swagger:SwaggerConfiguration:OpenApiInfo:Contact:Url")!)
                    }
                });
        });

        return services;
    }
}
