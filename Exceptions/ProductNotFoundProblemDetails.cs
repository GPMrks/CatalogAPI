using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Exceptions;

public class ProductNotFoundProblemDetails : ProblemDetails
{
    public ProductNotFoundProblemDetails(int id)
    {
        Title = "Product Not Found";
        Status = 404;
        Detail = "The requested Product was not found with id: " + id;
        Instance = "api/Products/" + id;
        Type = "api/Products/Not-Found";
        Extensions.Add("requestedId", id);
    }
}