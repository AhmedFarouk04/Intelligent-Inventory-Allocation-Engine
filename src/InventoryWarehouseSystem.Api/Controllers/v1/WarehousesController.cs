using InventoryWarehouseSystem.Application.Queries.Warehouses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWarehouseSystem.Api.Controllers.v1;

public class WarehousesController : BaseController
{
    private readonly IMediator _mediator;

    public WarehousesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var data = await _mediator.Send(new GetWarehousesQuery(), cancellationToken);
        return Ok(data);
    }
}
