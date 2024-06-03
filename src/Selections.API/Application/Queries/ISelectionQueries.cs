using Selections.API.Application.Queries.ViewModels;

namespace Selections.API.Application.Queries;

public interface ISelectionQueries
{
    Task<Selection?> GetAsync(Guid selectionId);
}