using MediatR;
using Selections.API.Application.Queries;

namespace Selections.API.Apis;

public class SelectionServices(
    IMediator mediator,
    ILogger<SelectionServices> logger,
    ISelectionQueries queries)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<SelectionServices> Logger { get; } = logger;
    public ISelectionQueries Queries { get; } = queries;
}