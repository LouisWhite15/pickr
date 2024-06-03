using MediatR;

namespace Selections.API.Application.Commands;

public record CancelSelectionCommand(Guid Id) : IRequest<bool>;
