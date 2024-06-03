using MediatR;
using Selections.Domain.Aggregates.SelectionAggregate;

namespace Selections.API.Application.Commands;

public class CreateSelectionCommandHandler(
    ILogger<CreateSelectionCommandHandler> logger,
    ISelectionRepository selectionRepository) 
    : IRequestHandler<CreateSelectionCommand, bool>
{
    public async Task<bool> Handle(CreateSelectionCommand request, CancellationToken cancellationToken)
    {
        var selection = new Selection(request.Name, request.UserId);

        foreach (var item in request.Items)
        {
            selection.AddSelectionItem(item.Id, item.Units);
        }
        
        logger.LogInformation("Creating Selection - SelectionId: {SelectionId}", selection.Id);

        selectionRepository.Add(selection);

        return await selectionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}