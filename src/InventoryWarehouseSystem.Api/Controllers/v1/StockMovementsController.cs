using InventoryWarehouseSystem.Application.Commands.StockMovements;
using InventoryWarehouseSystem.Application.Queries.StockMovements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWarehouseSystem.Api.Controllers.v1;

public class StockMovementsController : BaseController
{
    private readonly IMediator _mediator;

    public StockMovementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("allocate")]
    public async Task<IActionResult> Allocate([FromBody] AllocateStockCommand command)
    {
        var response = await _mediator.Send(command);
        return HandleApiResponse(response);
    }

    [HttpGet("history")]
    public async Task<IActionResult> History([FromQuery] int? productId, [FromQuery] int? warehouseId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var response = await _mediator.Send(new GetMovementHistoryQuery(productId, warehouseId, from, to));
        return Ok(response);
    }
}
