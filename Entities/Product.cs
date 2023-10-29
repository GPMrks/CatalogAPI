using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CatalogAPI.DTOs;

namespace CatalogAPI.Entities;

[Table("Products")]
public class Product
{

    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public float Stock { get; set; }
    public DateTime RegisterDate { get; set; }
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public Category? Category { get; set; }
    
    public Product()
    {
    }

    public Product(ProductForm productForm)
    {
        Name = productForm.Name;
        Description = productForm.Description;
        Price = productForm.Price;
        ImageUrl = productForm.ImageUrl;
        Stock = productForm.Stock;
        RegisterDate = DateTime.Now.ToUniversalTime();;
        CategoryId = productForm.CategoryId;
    }
}
