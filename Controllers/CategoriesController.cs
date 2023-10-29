using CatalogAPI.DTOs;
using CatalogAPI.Exceptions;
using CatalogAPI.Pagination;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;
    private readonly ILogger _logger;

    public CategoriesController(ICategoriesService categoriesService, ILogger<CategoriesController> logger)
    {
        _categoriesService = categoriesService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<CategoryDTO>> GetAllCategories([FromQuery] CategoriesParameters categoriesParameters)
    {
        _logger.LogInformation("******** GET api/categories ********");
        
        var categoriesDto = _categoriesService.GetAllCategories(categoriesParameters);

        if (categoriesDto is null) return NotFound();

        return Ok(categoriesDto);
    }
    
    [HttpGet("Products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<CategoryDTO>> GetAllCategoriesProducts()
    {
        _logger.LogInformation("******** GET api/categories/products ********");
        
        var categoriesAndProductsDto = _categoriesService.GetProductsInCategories();

        if (categoriesAndProductsDto is null) return NotFound();

        return Ok(categoriesAndProductsDto);
    }

    [HttpGet("{id:int}", Name = "GetCategoryById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CategoryDTO> GetCategoryById(int id)
    {
        _logger.LogInformation("******** GET api/categories/{Id} ********", id);
        
        try
        {
            CategoryDTO categoryDto = _categoriesService.GetCategoryById(id);
            return Ok(categoryDto);
        }
        catch (CategoryNotFoundException)
        {
            var problemDetails = new CategoryNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CategoryDTO> CreateCategory(CategoryForm categoryForm)
    {
        _logger.LogInformation($"******** POST api/categories ********");
        
        if (categoryForm is null) return BadRequest();
        CategoryDTO categoryDto = _categoriesService.CreateCategory(categoryForm);
        return new CreatedAtRouteResult("GetCategoryById", new { id = categoryDto.Id }, categoryDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CategoryDTO> UpdateCategory(int id, CategoryForm categoryForm)
    {
        _logger.LogInformation("******** PUT api/categories/{Id} ********", id);
        
        try
        {
            CategoryDTO categoryDto = _categoriesService.UpdateCategory(id, categoryForm);
            return Ok(categoryDto);
        }
        catch (CategoryNotFoundException)
        {
            var problemDetails = new CategoryNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
        catch (CannotUpdateCategoryException)
        {
            var problemDetails = new CannotUpdateCategoryProblemDetails(id);
            return BadRequest(problemDetails);
        }
    }
    
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CategoryDTO> UpdateCategoryPatch(int id, CategoryFormPatch categoryFormPatch)
    {
        _logger.LogInformation("******** PATCH api/categories/{Id} ********", id);
        
        try
        {
            CategoryDTO categoryDto = _categoriesService.UpdateCategoryPatch(id, categoryFormPatch);
            return Ok(categoryDto);
        }
        catch (CategoryNotFoundException)
        {
            var problemDetails = new CategoryNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
        catch (CannotUpdateCategoryException)
        {
            var problemDetails = new CannotUpdateCategoryProblemDetails(id);
            return BadRequest(problemDetails);
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteCategory(int id)
    {
        _logger.LogInformation("******** DELETE api/categories/{Id} ********", id);
        
        try
        {
            _categoriesService.DeleteCategory(id);
            return NoContent();
        }
        catch (CategoryNotFoundException)
        {
            var problemDetails = new CategoryNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
    }
}