using FluentAssertions;
using InventoryWarehouseSystem.Application.Commands.StockMovements;
using InventoryWarehouseSystem.Application.Exceptions;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Repositories;
using Moq;

namespace InventoryWarehouseSystem.UnitTests.Application;

public class AllocateStockHandlerTests
{
    [Fact]
    public async Task Handle_NotEnoughStock_ThrowsInsufficientStockException()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(x => x.Stocks.GetByProductAndWarehouseAsync(1, 1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Stock(1, 1, 2));

        var handler = new AllocateStockCommandHandler(unitOfWork.Object);

        var act = async () => await handler.Handle(new AllocateStockCommand(1, 1, 5, null, null, null), CancellationToken.None);
        await act.Should().ThrowAsync<InsufficientStockException>();
    }
}
