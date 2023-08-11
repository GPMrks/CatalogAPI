using CatalogAPI.Entities;
using CatalogAPI.Exceptions;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
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
    public async Task<ActionResult<List<Product>>> GetAllProductsAsync()
    {
        var products = await _productsService.FindAllProductsAsync();

        if (products is null) return NotFound();

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
    {
        try
        {
            var products = await _productsService.FindProductByIdAsync(id);
            return Ok(products);
        }
        catch (ProductNotFoundException)
        {
            var problemDetails = new ProductNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> CreateProductAsync(Product product)
    {
        if (product is null) return BadRequest();
        await _productsService.CreateProductAsync(product);
        return new CreatedAtRouteResult("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> UpdateProductAsync(int id, Product product)
    {
        if (id != product.Id) return BadRequest();

        try
        {
            await _productsService.UpdateProductAsync(id, product);
            return Ok(product);
        }
        catch (ProductNotFoundException)
        {
            var problemDetails = new ProductNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProductAsync(int id)
    {
        try
        {
            await _productsService.FindProductByIdAsync(id);
            await _productsService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (ProductNotFoundException)
        {
            var problemDetails = new ProductNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
    }
}