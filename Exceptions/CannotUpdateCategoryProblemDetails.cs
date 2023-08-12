using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Exceptions;

public class CannotUpdateCategoryProblemDetails : ProblemDetails
{
    public CannotUpdateCategoryProblemDetails(int id)
    {
        Title = "Cannot Update Category";
        Status = 400;
        Detail = "Fields were not filled correctly or may be missing for Category with id: " + id;
        Instance = "api/Category/" + id;
        Type = "api/Category/Not-Allowed";
        Extensions.Add("requestedId", id);
    }
}