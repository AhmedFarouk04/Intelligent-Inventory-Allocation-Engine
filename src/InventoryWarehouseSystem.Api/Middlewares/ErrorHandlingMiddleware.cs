using System.Text.Json;
using InventoryWarehouseSystem.Application.DTOs.Responses;
using InventoryWarehouseSystem.Application.Exceptions;
using InventoryWarehouseSystem.SharedKernel.Exceptions;

namespace InventoryWarehouseSystem.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var (statusCode, message, errors) = ex switch
            {
                ValidationException validation => (StatusCodes.Status400BadRequest, validation.Message, validation.Errors),
                NotFoundException notFound => (StatusCodes.Status404NotFound, notFound.Message, Array.Empty<string>()),
                InsufficientStockException insufficient => (StatusCodes.Status409Conflict, insufficient.Message, Array.Empty<string>()),
                ArgumentException argument => (StatusCodes.Status400BadRequest, argument.Message, Array.Empty<string>()),
                InvalidOperationException invalid => (StatusCodes.Status409Conflict, invalid.Message, Array.Empty<string>()),
                _ => (StatusCodes.Status500InternalServerError, "Internal server error", Array.Empty<string>())
            };

            if (statusCode >= 500)
            {
                _logger.LogError(ex, "Unhandled exception");
            }
            else
            {
                _logger.LogWarning(ex, "Request failed: {Message}", message);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var payload = JsonSerializer.Serialize(ApiResponse<string>.Failure(message, statusCode, errors), JsonOptions);
            await context.Response.WriteAsync(payload);
        }
    }
}
