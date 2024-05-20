using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace OrderAPI.Common.Framework.Models;

[ExcludeFromCodeCoverage]
[DataContract]
public class ExceptionApiResponse
{
    [DataMember(Name = "detail")]
    public string? Message { get; set; }

    [DataMember(Name = "developerMessage")]
    public string? DeveloperMessage { get; set; }

    [DataMember(Name = "title")]
    public string? Title { get; set; }

    public ExceptionApiResponse(string? message, string? title, string? developerMessage = null)
    {
        Message = message;
        DeveloperMessage = developerMessage;
        Title = title;
    }
}
