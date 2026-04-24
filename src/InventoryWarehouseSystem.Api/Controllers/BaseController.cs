using InventoryWarehouseSystem.Application.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWarehouseSystem.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleApiResponse<T>(ApiResponse<T> response)
        => StatusCode(response.StatusCode, response);
}
