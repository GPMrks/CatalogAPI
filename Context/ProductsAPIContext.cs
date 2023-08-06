using Microsoft.EntityFrameworkCore;
using ProductsAPI.Entities;

namespace ProductsAPI.Context;

public class ProductsApiContext : DbContext
{
    public ProductsApiContext(DbContextOptions<ProductsApiContext> options) : base(options)
    {
    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
}