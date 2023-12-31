using CatalogAPI.Entities;

namespace CatalogAPI.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }

    public ProductDTO()
    {
    }

    public ProductDTO(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        ImageUrl = product.ImageUrl;
        CategoryId = product.CategoryId;
    }
}