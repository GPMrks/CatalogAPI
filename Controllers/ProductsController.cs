using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Entities;
using ProductsAPI.Exceptions;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productsService.FindAllProductsAsync();

        if (products is null) return NotFound();
        
        return Ok(products);
    }
    
    [HttpGet("{id:int}", Name = "GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var products = await _productsService.FindProductByIdAsync(id);
            return Ok(products);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        if (product is null) return BadRequest();
        await _productsService.CreateProductAsync(product);
        return new CreatedAtRouteResult("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        
        try
        {
            await _productsService.UpdateProductAsync(id, product);
            return Ok(product);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            await _productsService.FindProductByIdAsync(id);
            await _productsService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}