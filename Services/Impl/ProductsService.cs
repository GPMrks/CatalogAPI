using CatalogAPI.Context;
using CatalogAPI.Entities;
using CatalogAPI.Entities.DTOs;
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

    public async Task<List<ProductDTO>> FindAllProductsAsync()
    {
        var products = await _catalogApiContext.Products.ToListAsync();
        return products.Select(product => new ProductDTO(product)).ToList();
    }

    public async Task<ProductDTO> FindProductByIdAsync(int id)
    {
        var product = await CheckIfProductExists(id);
        return new ProductDTO(product);
    }

    public async Task<ProductDTO> CreateProductAsync(ProductForm productForm)
    {
        Product product = new Product(productForm.Name, productForm.Description, productForm.Price, productForm.ImageUrl, productForm.Stock, productForm.RegisterDate, productForm.CategoryId);
        _catalogApiContext.Products.Add(product);
        await _catalogApiContext.SaveChangesAsync();
        return new ProductDTO(product);
    }

    public async Task<ProductDTO> UpdateProductAsync(int id, ProductForm productForm)
    {
        try
        {
            var product = await CheckIfProductExists(id);
            product.Name = productForm.Name ?? product.Name;
            product.Description = productForm.Description ?? product.Description;
            product.Price = (productForm.Price == 0) ? product.Price : productForm.Price;
            product.ImageUrl = productForm.ImageUrl ?? product.ImageUrl;
            product.Stock = (productForm.Stock == 0) ? product.Stock : productForm.Stock;
            product.RegisterDate = (productForm.RegisterDate == DateTime.MinValue) ? product.RegisterDate : productForm.RegisterDate;
            product.CategoryId = (productForm.CategoryId == 0) ? product.CategoryId : productForm.CategoryId;
            _catalogApiContext.Entry(product).State = EntityState.Modified;
            await _catalogApiContext.SaveChangesAsync();
            return new ProductDTO(product);
        }
        catch (DbUpdateException)
        {
            throw new CannotUpdateProductException();
        }
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