namespace ProductsAPI.Exceptions;

public class ProductNotFoundException : ApplicationException
{
    public ProductNotFoundException(string? message) : base(message)
    {
    }
}