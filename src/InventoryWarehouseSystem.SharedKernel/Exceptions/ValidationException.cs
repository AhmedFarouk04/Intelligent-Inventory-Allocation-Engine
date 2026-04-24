namespace InventoryWarehouseSystem.SharedKernel.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyCollection<string> Errors { get; }

    public ValidationException(string message, IEnumerable<string>? errors = null) : base(message)
    {
        Errors = errors is null ? Array.Empty<string>() : errors.ToList().AsReadOnly();
    }
}
