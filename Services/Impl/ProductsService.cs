using CatalogAPI.Context;
using CatalogAPI.DTOs;
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
            product.Name = productForm.Name;
            product.Description = productForm.Description;
            product.Price = productForm.Price;
            product.ImageUrl = productForm.ImageUrl;
            product.Stock = productForm.Stock;
            product.RegisterDate = productForm.RegisterDate;
            product.CategoryId = productForm.CategoryId;
            _catalogApiContext.Entry(product).State = EntityState.Modified;
            await _catalogApiContext.SaveChangesAsync();
            return new ProductDTO(product);
        }
        catch (DbUpdateException)
        {
            throw new CannotUpdateProductException();
        }
    }

    public async Task<ProductDTO> UpdateProductPatchAsync(int id, ProductFormPatch productFormPatch)
    {
        try
        {
            var product = await CheckIfProductExists(id);
            product.Name = productFormPatch.Name ?? product.Name;
            product.Description = productFormPatch.Description ?? product.Description;
            product.Price = (productFormPatch.Price == 0) ? product.Price : productFormPatch.Price;
            product.ImageUrl = productFormPatch.ImageUrl ?? product.ImageUrl;
            product.Stock = (productFormPatch.Stock == 0) ? product.Stock : productFormPatch.Stock;
            product.RegisterDate = (productFormPatch.RegisterDate == DateTime.MinValue) ? product.RegisterDate : productFormPatch.RegisterDate;
            product.CategoryId = (productFormPatch.CategoryId == 0) ? product.CategoryId : productFormPatch.CategoryId;
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