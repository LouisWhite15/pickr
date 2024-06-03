using Selections.Domain.Events;
using Selections.Domain.SeedWork;

namespace Selections.Domain.Aggregates.SelectionAggregate;

public sealed class Selection : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public List<SelectionItem> Items { get; private set; } = [];
    public Guid UserId { get; private set; }
    
    public SelectionStatus Status { get; private set; }

    public Selection()
    {
    }

    public Selection(string name, List<SelectionItem> items, Guid userId)
    {
        Id = Guid.NewGuid();
        Items = items;
        UserId = userId;
        Status = SelectionStatus.Started;
        
        AddDomainEvent(new SelectionStartedDomainEvent(this));
    }
}