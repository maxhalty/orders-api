using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace OrderAPI.Infraestructure.Framework.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public sealed class EnvironmentVariableNotFoundException : BaseException
{
    public EnvironmentVariableNotFoundException(string environmentVariableName) 
        : base("Environment variable not found")
    {
        base.Title = "EnvironmentVariableNotFoundException";
        base.DeveloperMessage = "Variable Name: " + environmentVariableName;
    }

    private EnvironmentVariableNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
