using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Entities;
using ProductsAPI.Exceptions;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Category>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoriesService.FindAllCategoriesAsync();

        if (categories is null) return NotFound();

        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategoryById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        try
        {
            var category = await _categoriesService.FindCategoryByIdAsync(id);
            return Ok(category);
        }
        catch (CategoryNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (category is null) return BadRequest();
        await _categoriesService.CreateCategoryAsync(category);
        return new CreatedAtRouteResult("GetCategoryById", new { id = category.Id }, category);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategory(int id, Category category)
    {
        if (id != category.Id) return BadRequest();

        try
        {
            await _categoriesService.UpdateCategoryAsync(id, category);
            return Ok(category);
        }
        catch (CategoryNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _categoriesService.FindCategoryByIdAsync(id);
            await _categoriesService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (CategoryNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}