using FluentAssertions;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Events;
using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using InventoryWarehouseSystem.Infrastructure.Repositories;
using InventoryWarehouseSystem.SharedKernel.Events;
using Microsoft.EntityFrameworkCore;

namespace InventoryWarehouseSystem.IntegrationTests.Messaging;

public class DomainEventDispatchTests
{
    private sealed class FakeDomainEventDispatcher : IDomainEventDispatcher
    {
        public List<InventoryWarehouseSystem.SharedKernel.Events.DomainEvent> Dispatched { get; } = new();

        public Task DispatchAsync(IEnumerable<InventoryWarehouseSystem.SharedKernel.Events.DomainEvent> events, CancellationToken cancellationToken = default)
        {
            Dispatched.AddRange(events);
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task SaveChangesAsync_DispatchesAndClearsDomainEvents()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"domain-events-{Guid.NewGuid()}")
            .Options;

        await using var context = new ApplicationDbContext(options);
        var dispatcher = new FakeDomainEventDispatcher();
        using var unitOfWork = new UnitOfWork(context, dispatcher);

        var category = new Category("Electronics");
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var product = Product.Create("SKU-TEST", "Keyboard", category.Id, 5);
        await unitOfWork.Products.AddAsync(product);

        await unitOfWork.SaveChangesAsync();

        dispatcher.Dispatched.Should().Contain(e => e is ProductCreatedEvent);
        product.DomainEvents.Should().BeEmpty();
    }
}
