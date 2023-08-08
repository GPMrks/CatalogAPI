using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Exceptions;

public class ProductNotFoundProblemDetails : ProblemDetails
{
    public ProductNotFoundProblemDetails(int id)
    {
        Title = "Product Not Found";
        Status = 404;
        Detail = "The requested Product was not found with id: " + id;
        Instance = "api/products/" + id;
        Type = "api/products/not-found";
        Extensions.Add("requestedId", id);
    }
}