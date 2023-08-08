using CatalogAPI.Context;
using CatalogAPI.Entities;
using CatalogAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services.Impl;

public class ProductsService : IProductsService
{
    private readonly CatalogApiContext _catalogApiContext;

    public ProductsService(CatalogApiContext catalogApiContext)
    {
        _catalogApiContext = catalogApiContext;
    }

    public async Task<List<Product>> FindAllProductsAsync()
    {
        var products = await _catalogApiContext.Products.ToListAsync();
        return products;
    }

    public async Task<Product> FindProductByIdAsync(int id)
    {
        return await CheckIfProductExists(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _catalogApiContext.Products.Add(product);
        await _catalogApiContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(int id, Product product)
    {
        await CheckIfProductExists(id);
        _catalogApiContext.Entry(product).State = EntityState.Modified;
        await _catalogApiContext.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await CheckIfProductExists(id);
        _catalogApiContext.Remove(product);
        await _catalogApiContext.SaveChangesAsync();
    }

    private async Task<Product> CheckIfProductExists(int id)
    {
        var product = await _catalogApiContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) throw new ProductNotFoundException("Product not found with ID: " + id);

        return product;
    }
}