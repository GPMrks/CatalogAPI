using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Exceptions;

public class CannotUpdateProductProblemDetails : ProblemDetails
{
    public CannotUpdateProductProblemDetails(int id)
    {
        Title = "Cannot Update Product";
        Status = 400;
        Detail = "Fields were not filled correctly or may be missing for Product with id: " + id;
        Instance = "api/Products/" + id;
        Type = "api/Products/Not-Allowed";
        Extensions.Add("requestedId", id);
    }
}