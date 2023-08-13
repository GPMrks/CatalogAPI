using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Exceptions;

public class CategoryNotFoundProblemDetails : ProblemDetails
{
    public CategoryNotFoundProblemDetails(int id)
    {
        Title = "Category Not Found";
        Status = 404;
        Detail = "The requested Category was not found with id: " + id;
        Instance = "api/Categories/" + id;
        Type = "api/Categories/Not-Found";
        Extensions.Add("requestedId", id);
    }
}