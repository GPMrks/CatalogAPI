namespace CatalogAPI.Exceptions;

public class ProductNotFoundException : ApplicationException
{
    public ProductNotFoundException(string? message) : base(message)
    {
    }
}