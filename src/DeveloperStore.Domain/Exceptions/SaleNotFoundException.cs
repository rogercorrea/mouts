namespace DeveloperStore.Domain.Exceptions;

public class SaleNotFoundException : Exception
{
    public SaleNotFoundException(Guid id)
        : base($"Sale '{id}' was not found.")
    {
    }
}