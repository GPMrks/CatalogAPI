namespace CatalogAPI.Exceptions;

public class CategoryNotFoundException : ApplicationException
{
    public CategoryNotFoundException(string? message) : base(message)
    {
    }
}