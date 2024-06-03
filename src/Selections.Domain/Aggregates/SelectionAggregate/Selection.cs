using Selections.Domain.Events;
using Selections.Domain.Exceptions;
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

    public Selection(string name, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        UserId = userId;
        Status = SelectionStatus.Started;
        
        AddDomainEvent(new SelectionStartedDomainEvent(this));
    }

    public void AddSelectionItem(Guid id, int units = 1)
    {
        var existingItem = Items.SingleOrDefault(item => item.Id == id);

        if (existingItem is not null)
        {
            existingItem.AddUnits(units);
        }
        else
        {
            var selectionItem = new SelectionItem(id, units);
            Items.Add(selectionItem);
        }
    }

    public void Cancel()
    {
        if (Status is SelectionStatus.Completed)
            StatusChangeException(SelectionStatus.Cancelled);

        Status = SelectionStatus.Cancelled;
        AddDomainEvent(new SelectionCancelledDomainEvent(this));
    }

    private void StatusChangeException(SelectionStatus attemptedOrderStatusChange)
    {
        throw new SelectionsDomainException(
            $"It is not possible to change the selection status from {Status} to {attemptedOrderStatusChange}");
    }
}