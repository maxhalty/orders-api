using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace OrderAPI.Infraestructure.Framework.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public sealed class OrderNotFoundException : BaseException
{
    public OrderNotFoundException(Guid orderId) : base($"No order found with Id: {orderId}")
    {
        Title = nameof(OrderNotFoundException);
    }

    private OrderNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
