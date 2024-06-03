using MediatR;
using Selections.Domain.Aggregates.SelectionAggregate;

namespace Selections.API.Application.Commands;

public class CancelSelectionCommandHandler(ISelectionRepository selectionRepository)
    : IRequestHandler<CancelSelectionCommand, bool>
{
    public async Task<bool> Handle(CancelSelectionCommand command, CancellationToken cancellationToken)
    {
        var selectionToUpdate = await selectionRepository.GetAsync(command.Id);
        if (selectionToUpdate is null)
            return false;
        
        selectionToUpdate.Cancel();
        return await selectionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}