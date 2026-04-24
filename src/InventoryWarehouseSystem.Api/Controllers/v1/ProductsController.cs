using InventoryWarehouseSystem.Application.Commands.Products;
using InventoryWarehouseSystem.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWarehouseSystem.Api.Controllers.v1;

public class ProductsController : BaseController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var response = await _mediator.Send(command);
        return HandleApiResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _mediator.Send(new GetProductsQuery(page, pageSize));
        return Ok(response);
    }
}
