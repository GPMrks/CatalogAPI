namespace ProductsAPI.Exceptions;

public class CategoryNotFoundException : ApplicationException
{
    public CategoryNotFoundException(string? message) : base(message)
    {
    }
}