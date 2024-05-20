using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace OrderAPI.Infraestructure.Framework.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class BaseException : Exception
{
    public string Title { get; set; } = "Unexpected Exception";

    public string? DeveloperMessage { get; set; }

    protected BaseException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

    protected BaseException(string message, string? developerMessage = null)
        : base(message)
    {
        DeveloperMessage = developerMessage;
    }

    protected BaseException(string message, Exception innerException, string? developerMessage = null)
        : base(message, innerException)
    {
        DeveloperMessage = developerMessage;
    }
}