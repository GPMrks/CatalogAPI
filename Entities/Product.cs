namespace ProductsAPI.Entities;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public float Stock { get; set; }
    public DateTime RegisterDate { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public Product()
    {
    }

    public Product(int id, string name, string description, decimal price, string imageUrl, float stock, DateTime registerDate)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        Stock = stock;
        RegisterDate = registerDate;
    }
}