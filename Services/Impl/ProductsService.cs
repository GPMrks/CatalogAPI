using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Exceptions;
using CatalogAPI.Pagination;
using CatalogAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services.Impl;

public class ProductsService : IProductsService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<ProductDTO> GetAllProducts(ProductsParameters productsParameters)
    {
        var products = _unitOfWork.ProductRepository.GetProducts(productsParameters);
        
        return products.Select(product => new ProductDTO(product)).ToList();
    }

    public ProductDTO GetProductById(int id)
    {
        var product = CheckIfProductExists(id);
        return new ProductDTO(product);
    }

    public List<ProductDTO> GetProductsSortedByPrice()
    {
        var products = _unitOfWork.ProductRepository.GetProductsSortedByPrice().ToList();
        return products.Select(product => new ProductDTO(product)).ToList();
    }

    public ProductDTO CreateProduct(ProductForm productForm)
    {
        Product product = new Product(productForm);
        _unitOfWork.ProductRepository.Add(product);
        _unitOfWork.Commit();
        return new ProductDTO(product);
    }

    public ProductDTO UpdateProduct(int id, ProductForm productForm)
    {
        try
        {
            var product = CheckIfProductExists(id);
            product.Name = productForm.Name;
            product.Description = productForm.Description;
            product.Price = productForm.Price;
            product.ImageUrl = productForm.ImageUrl;
            product.Stock = productForm.Stock;
            product.CategoryId = productForm.CategoryId;
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();
            return new ProductDTO(product);
        }
        catch (DbUpdateException)
        {
            throw new CannotUpdateProductException();
        }
    }

    public ProductDTO UpdateProductPatch(int id, ProductFormPatch productFormPatch)
    {
        try
        {
            var product = CheckIfProductExists(id);
            product.Name = productFormPatch.Name ?? product.Name;
            product.Description = productFormPatch.Description ?? product.Description;
            product.Price = (productFormPatch.Price == 0) ? product.Price : productFormPatch.Price;
            product.ImageUrl = productFormPatch.ImageUrl ?? product.ImageUrl;
            product.Stock = (productFormPatch.Stock == 0) ? product.Stock : productFormPatch.Stock;
            product.RegisterDate = (productFormPatch.RegisterDate == DateTime.MinValue) ? product.RegisterDate : productFormPatch.RegisterDate;
            product.CategoryId = (productFormPatch.CategoryId == 0) ? product.CategoryId : productFormPatch.CategoryId;
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();
            return new ProductDTO(product);
        }
        catch (DbUpdateException)
        {
            throw new CannotUpdateProductException();
        }
    }

    public void DeleteProduct(int id)
    {
        var product = CheckIfProductExists(id);
        _unitOfWork.ProductRepository.Delete(product);
        _unitOfWork.Commit();
    }
 
    private Product CheckIfProductExists(int id)
    {
        var product = _unitOfWork.ProductRepository.FindById(p => p.Id == id);

        if (product is null) throw new ProductNotFoundException("Product not found with ID: " + id);

        return product;
    }
}