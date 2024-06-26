﻿namespace OrderAPI.Domain;

public class Order
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    public Order(Guid id, string name, string description, decimal price, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CreatedAt = createdAt;
    }

    public Order(string name, string description, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        CreatedAt = DateTime.Now;
    }
}
