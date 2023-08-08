using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Exceptions;

public class CategoryNotFoundProblemDetails : ProblemDetails
{
    public CategoryNotFoundProblemDetails(int id)
    {
        Title = "Category Not Found";
        Status = 404;
        Detail = "The requested Category was not found with id: " + id;
        Instance = "api/categories/" + id;
        Type = "api/categories/not-found";
        Extensions.Add("requestedId", id);
    }
}