namespace CatalogAPI.Entities.DTOs;

public class ProductForm
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public float Stock { get; set; }
    public DateTime RegisterDate { get; set; }
    public int CategoryId { get; set; }
}