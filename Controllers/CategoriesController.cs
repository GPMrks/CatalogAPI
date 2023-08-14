using CatalogAPI.DTOs;
using CatalogAPI.Exceptions;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<CategoryDTO>>> GetAllCategoriesAsync()
    {
        var categoriesDto = await _categoriesService.FindAllCategoriesAsync();

        if (categoriesDto is null) return NotFound();

        return Ok(categoriesDto);
    }
    
    [HttpGet("Products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<CategoryDTO>>> GetAllCategoriesProductsAsync()
    {
        var categoriesAndProductsDto = await _categoriesService.FindProductsInCategories();

        if (categoriesAndProductsDto is null) return NotFound();

        return Ok(categoriesAndProductsDto);
    }

    [HttpGet("{id:int}", Name = "GetCategoryById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDTO>> GetCategoryByIdAsync(int id)
    {
        try
        {
            CategoryDTO categoryDto = await _categoriesService.FindCategoryByIdAsync(id);
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
    public async Task<ActionResult<CategoryDTO>> CreateCategoryAsync(CategoryForm categoryForm)
    {
        if (categoryForm is null) return BadRequest();
        CategoryDTO categoryDto = await _categoriesService.CreateCategoryAsync(categoryForm);
        return new CreatedAtRouteResult("GetCategoryById", new { id = categoryDto.Id }, categoryDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDTO>> UpdateCategoryAsync(int id, CategoryForm categoryForm)
    {
        try
        {
            CategoryDTO categoryDto = await _categoriesService.UpdateCategoryAsync(id, categoryForm);
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
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    {
        try
        {
            await _categoriesService.FindCategoryByIdAsync(id);
            await _categoriesService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (CategoryNotFoundException)
        {
            var problemDetails = new CategoryNotFoundProblemDetails(id);
            return NotFound(problemDetails);
        }
    }
}