using FluentAssertions;
using InventoryWarehouseSystem.Application.Commands.Products;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Repositories;
using Moq;

namespace InventoryWarehouseSystem.UnitTests.Application;

public class CreateProductHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedResponse()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(x => x.Products.GetBySkuAsync("SKU-100", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);
        unitOfWork.Setup(x => x.Products.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateProductCommandHandler(unitOfWork.Object);

        var response = await handler.Handle(new CreateProductCommand("SKU-100", "Mouse", null, 1, 5), CancellationToken.None);

        response.Success.Should().BeTrue();
        response.StatusCode.Should().Be(201);
    }
}
