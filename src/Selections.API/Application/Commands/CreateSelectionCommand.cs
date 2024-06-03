using MediatR;
using Selections.API.Apis.Requests;
using Selections.Domain.Aggregates.SelectionAggregate;

namespace Selections.API.Application.Commands;

public record CreateSelectionCommand(
    string Name,
    List<SelectionItemDTO> Items,
    Guid UserId) : IRequest<bool>;
