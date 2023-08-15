using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs;

public class ProductFormPatch
{
    [MaxLength(80)] 
    public string? Name { get; set; }
    
    [MaxLength(300)] 
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    [MaxLength(300)] 
    public string? ImageUrl { get; set; }
    public float Stock { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime RegisterDate { get; set; } = DateTime.Now.ToUniversalTime();
    
    public int CategoryId { get; set; }
}