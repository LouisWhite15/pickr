namespace Selections.API.Application.Queries.ViewModels;

public record Selection(Guid Id, List<SelectionItem> Items, string Status);

public record SelectionItem(Guid Id);