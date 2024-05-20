using OrderAPI.Infraestructure.Framework.Exceptions;

namespace OrderAPI.Infraestructure.Framework.Helpers;

public static class EnvironmentHelper
{
    public static string GetEnvironmentVariable(string variableName)
    {
        return Environment.GetEnvironmentVariable(variableName) ?? throw new EnvironmentVariableNotFoundException(variableName);
    }
}
