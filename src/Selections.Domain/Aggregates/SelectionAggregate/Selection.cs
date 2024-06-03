using Selections.Domain.Events;
using Selections.Domain.SeedWork;

namespace Selections.Domain.Aggregates.SelectionAggregate;

public class Selection : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public List<SelectionItem> Items { get; set; } = [];
    public Guid UserId { get; set; }

    public Selection()
    {
    }

    public Selection(string name, List<SelectionItem> items, Guid userId)
    {
        Id = Guid.NewGuid();
        Items = items;
        UserId = userId;
        
        AddDomainEvent(new SelectionStartedDomainEvent(this));
    }
}