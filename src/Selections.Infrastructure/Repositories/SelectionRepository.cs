using Microsoft.EntityFrameworkCore;
using Selections.Domain.Aggregates.SelectionAggregate;
using Selections.Domain.SeedWork;

namespace Selections.Infrastructure.Repositories;

public class SelectionRepository(SelectionsContext context) : ISelectionRepository
{
    public IUnitOfWork UnitOfWork => context;

    public Selection Add(Selection selection)
    {
        return context.Selections.Add(selection).Entity;
    }

    public void Update(Selection selection)
    {
        context.Entry(selection).State = EntityState.Modified;
    }

    public async Task<Selection?> GetAsync(Guid selectionId)
    {
        var selection = await context.Selections.FindAsync(selectionId);

        if (selection is not null)
        {
            await context.Entry(selection)
                .Collection(i => i.Items)
                .LoadAsync();
        }

        return selection;
    }

    public async Task<List<Selection>> GetAsync()
    {
        return await context.Selections
            .Include(selection => selection.Items)
            .ToListAsync();
    }
}