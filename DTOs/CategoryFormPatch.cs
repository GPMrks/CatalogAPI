using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs;

public class CategoryFormPatch
{
    [MaxLength(80)]
    public string? Name { get; set; }
    
    [MaxLength(300)]
    public string? ImageUrl { get; set; }
}