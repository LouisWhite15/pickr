using Selections.Domain.SeedWork;

namespace Selections.Domain.Aggregates.SelectionAggregate;

public interface ISelectionRepository : IRepository<Selection>
{
    Selection Add(Selection selection);
    void Update(Selection selection);
    Task<Selection?> GetAsync(Guid selectionId);
    Task<List<Selection>> GetAsync();
}