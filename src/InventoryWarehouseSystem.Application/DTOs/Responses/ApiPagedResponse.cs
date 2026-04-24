namespace InventoryWarehouseSystem.Application.DTOs.Responses;

public class ApiPagedResponse<T>
{
    public bool Success { get; set; }
    public IReadOnlyCollection<T> Data { get; set; } = Array.Empty<T>();
    public string Message { get; set; } = "Success";
    public int StatusCode { get; set; } = 200;
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
