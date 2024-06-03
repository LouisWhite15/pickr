namespace Selections.API.Application.Queries.ViewModels;

public record Selection(Guid Id, string Name, List<SelectionItem> Items, string Status);

public record SelectionItem(Guid Id, int Units);