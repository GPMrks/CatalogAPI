using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs;

public class CategoryForm
{
    [Required]
    [MaxLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [MaxLength(300)]
    public string? ImageUrl { get; set; }
}