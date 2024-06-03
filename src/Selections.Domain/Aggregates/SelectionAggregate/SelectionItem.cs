using Selections.Domain.Exceptions;
using Selections.Domain.SeedWork;

namespace Selections.Domain.Aggregates.SelectionAggregate;

public sealed class SelectionItem : Entity
{
    public int Units { get; private set; }
    
    public SelectionItem(Guid id)
    {
        Id = id;
        Units = 0;
    }

    public SelectionItem(Guid id, int units)
    {
        Id = id;
        Units = units;
    }

    public void AddUnits(int units)
    {
        if (units < 0)
            throw new SelectionsDomainException("Invalid units");
        
        Units += units;
    }

    public void RemoveUnits(int units)
    {
        if (units < 0)
            throw new SelectionsDomainException("Invalid units");

        Units -= units;
    }
}