using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsAPI.Entities;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [MaxLength(300)]
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; } = new Collection<Product>();
}