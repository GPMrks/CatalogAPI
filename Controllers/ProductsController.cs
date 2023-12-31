using CatalogAPI.DTOs;
using CatalogAPI.Exceptions;
using CatalogAPI.Filters;
using CatalogAPI.Pagination;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly ILogger _logger;

    public ProductsController(IProductsService productsService, ILogger<CategoriesController> logger)
    {
        _productsService = productsService;
        _logger = logger;

    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<ProductDTO>> GetAllProducts([FromQuery] ProductsParameters productsParameters)
    {
        _logger.LogInformation("******** GET api/products ********");
        
        var productsDtos = _productsService.GetAllProducts(productsParameters);

        if (productsDtos is null) return NotFound();

        return Ok(productsDtos);
    }
    
    [HttpGet("sorted-by-price")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<ProductDTO>> GetAllProductsSortedByPrice()
    {
        _logger.LogInformation("******** GET api/products/sorted-by-price ********");
        
        var productsDtos = _productsService.GetProductsSortedByPrice();

        if (productsDtos is null) return NotFound();

        return Ok(productsDtos);
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProductDTO> GetProductById(int id)
    {
        
        _logger.LogInformation("******** GET api/products/{id} ********");
            
        try
        {
            
            var productDto = _productsService.GetProductById(id);
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
    public ActionResult<ProductDTO> CreateProduct(ProductForm productForm)
    {
        _logger.LogInformation("******** POST api/products ********");
        
        if (productForm is null) return BadRequest();
        var productDto = _productsService.CreateProduct(productForm);
        return new CreatedAtRouteResult("GetProductById", new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductDTO> UpdateProduct(int id, ProductForm productForm)
    {
        
        _logger.LogInformation("******** PUT api/products/{id} ********");
        
        try
        {
            var productDto = _productsService.UpdateProduct(id, productForm);
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
    
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductDTO> UpdateProductPatch(int id, ProductFormPatch productFormPatch)
    {
        _logger.LogInformation("******** PATCH api/products/{id} ********");
        
        try
        {
            var productDto = _productsService.UpdateProductPatch(id, productFormPatch);
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
    public ActionResult DeleteProduct(int id)
    {
        _logger.LogInformation("******** DELETE api/products/{id} ********");
        
        try
        {
            _productsService.DeleteProduct(id);
            return NoContent();
        }
        catch (ProductNotFoundException)
        {
            var problemDetails = new ProductNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
    }
}