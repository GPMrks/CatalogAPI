using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Context;

public class CatalogApiContext : DbContext
{
    public CatalogApiContext(DbContextOptions<CatalogApiContext> options) : base(options)
    {
    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
}