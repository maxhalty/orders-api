using Microsoft.AspNetCore.Http;

namespace OrderAPI.Application.Extensions;

public static class HttpRequestExtensions
{
    public static Uri GetUriByPath(this HttpRequest httRequest, string path)
    {
        UriBuilder uriBuilder = new()
        {
            Scheme = httRequest.Scheme,
            Host = httRequest.Host.Value,
            Path = path
        };

        return uriBuilder.GetCleanUriOfBracketsFromUriBuilder();
    }

    private static Uri GetCleanUriOfBracketsFromUriBuilder(this UriBuilder uriBuilder)
    {
        string uriValue = uriBuilder
            .ToString()
            .Replace("[", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("]", string.Empty, StringComparison.OrdinalIgnoreCase);

        return new(uriValue);
    }
}
