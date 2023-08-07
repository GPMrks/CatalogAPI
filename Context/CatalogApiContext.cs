using Microsoft.EntityFrameworkCore;
using ProductsAPI.Entities;

namespace ProductsAPI.Context;

public class CatalogApiContext : DbContext
{
    public CatalogApiContext(DbContextOptions<CatalogApiContext> options) : base(options)
    {
    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
}