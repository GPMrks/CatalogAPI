using CatalogAPI.Entities;
using CatalogAPI.Entities.DTOs;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductDTO>>> GetAllProductsAsync()
    {
        var productsDtos = await _productsService.FindAllProductsAsync();

        if (productsDtos is null) return NotFound();

        return Ok(productsDtos);
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
    {
        try
        {
            var productDto = await _productsService.FindProductByIdAsync(id);
            return Ok(productDto);
        }
        catch (ProductNotFoundException)
        {
            var problemDetails = new ProductNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDTO>> CreateProductAsync(ProductForm productForm)
    {
        if (productForm is null) return BadRequest();
        var productDto = await _productsService.CreateProductAsync(productForm);
        return new CreatedAtRouteResult("GetProductById", new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDTO>> UpdateProductAsync(int id, ProductForm productForm)
    {
        try
        {
            var productDto = await _productsService.UpdateProductAsync(id, productForm);
            return Ok(productDto);
        }
        catch (ProductNotFoundException)
        {
            var problemDetails = new ProductNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
        catch (CannotUpdateProductException)
        {
            var problemDetails = new CannotUpdateProductProblemDetails(id);
            return BadRequest(problemDetails);
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