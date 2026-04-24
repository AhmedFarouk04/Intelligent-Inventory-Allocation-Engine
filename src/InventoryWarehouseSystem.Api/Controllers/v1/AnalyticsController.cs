using InventoryWarehouseSystem.Application.Queries.Analytics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWarehouseSystem.Api.Controllers.v1;

public class AnalyticsController : BaseController
{
    private readonly IMediator _mediator;

    public AnalyticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("low-stock-alerts")]
    public async Task<IActionResult> GetLowStockAlerts()
    {
        var data = await _mediator.Send(new GetLowStockAlertsQuery());
        return Ok(data);
    }
}
