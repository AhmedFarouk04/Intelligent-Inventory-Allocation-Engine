namespace InventoryWarehouseSystem.Application.DTOs.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public IReadOnlyCollection<string> Errors { get; set; } = Array.Empty<string>();
    public int StatusCode { get; set; }

    public static ApiResponse<T> SuccessResponse(T? data, string message = "Success", int statusCode = 200)
        => new() { Success = true, Data = data, Message = message, StatusCode = statusCode };

    public static ApiResponse<T> Failure(string message, int statusCode = 400, IEnumerable<string>? errors = null)
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = statusCode,
            Errors = errors is null ? Array.Empty<string>() : errors.ToArray()
        };
}
