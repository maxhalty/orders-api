using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using OrderAPI.Common.Framework.Models;
using OrderAPI.Infraestructure.Framework.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace OrderAPI.Common.Framework.Handlers;

[ExcludeFromCodeCoverage]
public static class ExceptionHandler
{
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app, IDictionary<string, HttpStatusCode> exceptionDictionary)
    {
        IDictionary<string, HttpStatusCode> exceptionDictionaryAux = exceptionDictionary;
        _ = app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = async delegate (HttpContext context)
            {
                IExceptionHandlerPathFeature? exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                Exception? exception = (exceptionHandlerPathFeature?.Error is EnvironmentVariableNotFoundException) ? exceptionHandlerPathFeature.Error.InnerException : exceptionHandlerPathFeature?.Error;
                string exceptionType = exception!.GetType().Name;
                string? developerMesage = null;
                if (exception.GetType().IsAssignableTo(typeof(BaseException)))
                {
                    developerMesage = ((BaseException)exception.GetBaseException()).DeveloperMessage;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = exceptionDictionaryAux.MapStatusCode(exceptionType);
                await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionApiResponse(exceptionType, exception.Message, developerMesage)));
            }
        });
        return app;
    }

    private static int MapStatusCode(this IDictionary<string, HttpStatusCode> exceptionDictionary, string exceptionType)
    {
        string exceptionTypeAux = exceptionType;
        return (int)exceptionDictionary.FirstOrDefault((KeyValuePair<string, HttpStatusCode> o) => o.Key == exceptionTypeAux, new KeyValuePair<string, HttpStatusCode>("InternalServerError", HttpStatusCode.InternalServerError)).Value;
    }
}
