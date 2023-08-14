using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs;

public class ProductForm
{
    [Required] 
    [MaxLength(80)] 
    public string? Name { get; set; }
    
    [Required] 
    [MaxLength(300)] 
    public string? Description { get; set; }
    
    [Required]
    [Range(1, 9999999999999999.99, ErrorMessage = "Price is required!")]
    public decimal Price { get; set; }
    
    [Required] 
    [MaxLength(300)] 
    public string? ImageUrl { get; set; }
    
    [Required]
    [Range(1, float.MaxValue, ErrorMessage = "Stock is required!")]
    public float Stock { get; set; }
    
    [Required(ErrorMessage = "Register date is required!")]
    [DataType(DataType.DateTime)]
    public DateTime RegisterDate { get; set; } = DateTime.Now.ToUniversalTime();
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId is required!")]
    public int CategoryId { get; set; }
}