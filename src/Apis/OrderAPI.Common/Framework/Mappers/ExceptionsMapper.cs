using OrderAPI.Infraestructure.Framework.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace OrderAPI.Common.Framework.Mappers;

[ExcludeFromCodeCoverage]
public static class ExceptionsMapper
{
    public static IDictionary<string, HttpStatusCode> GetApiDictionary()
    {
        return new Dictionary<string, HttpStatusCode>()
        {
            { nameof(EnvironmentVariableNotFoundException), HttpStatusCode.NotFound }
        };
    }
}
