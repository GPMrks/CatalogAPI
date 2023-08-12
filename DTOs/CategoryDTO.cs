using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace CatalogAPI.Entities.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Product>? Products { get; set; }

    public CategoryDTO()
    {
    }

    public CategoryDTO(Category category)
    {
        Id = category.Id;
        Name = category.Name;
        ImageUrl = category.ImageUrl;
        Products = category.Products;
    }
}