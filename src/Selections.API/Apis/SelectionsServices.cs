using MediatR;
using Selections.API.Application.Queries;

namespace Selections.API.Apis;

public class SelectionsServices(
    IMediator mediator,
    ILogger<SelectionsServices> logger,
    ISelectionQueries queries)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<SelectionsServices> Logger { get; } = logger;
    public ISelectionQueries Queries { get; } = queries;
}