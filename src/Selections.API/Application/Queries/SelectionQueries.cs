using Selections.Domain.Aggregates.SelectionAggregate;
using Selection = Selections.API.Application.Queries.ViewModels.Selection;
using SelectionItem = Selections.API.Application.Queries.ViewModels.SelectionItem;

namespace Selections.API.Application.Queries;

public class SelectionQueries(ISelectionRepository repository) : ISelectionQueries
{
    public async Task<Selection?> GetAsync(Guid selectionId)
    {
        var selection = await repository.GetAsync(selectionId);

        if (selection is null)
            return null;

        return new Selection(
            selection.Id,
            selection.Name,
            selection.Items.Select(item => new SelectionItem(item.Id, item.Units)).ToList(),
            selection.Status.ToString());
    }
    
    public async Task<List<Selection>> GetAsync()
    {
        var selections = await repository.GetAsync();
        
        return selections.Select(selection => 
            new Selection(
                selection.Id,
                selection.Name,
                selection.Items.Select(i => new SelectionItem(i.Id, i.Units)).ToList(),
                selection.Status.ToString())).ToList();
    }
}