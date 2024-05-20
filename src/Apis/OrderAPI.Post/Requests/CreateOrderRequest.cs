namespace OrderAPI.Post.Requests;

public class CreateOrderRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}
