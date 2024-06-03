using Selections.API.Application.Queries.ViewModels;

namespace Selections.API.Apis.Requests;

public record CreateSelectionRequest(
    string Name,
    List<SelectionItemDTO> Items,
    Guid UserId);

public record SelectionItemDTO(Guid Id, int Units);