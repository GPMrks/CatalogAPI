using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace ProductsAPI.Entities;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; } = new Collection<Product>();
}