using System.ComponentModel.DataAnnotations.Schema;
using InventoryWarehouseSystem.SharedKernel.Events;

namespace InventoryWarehouseSystem.SharedKernel.Base;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
