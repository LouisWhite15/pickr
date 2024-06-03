using Microsoft.AspNetCore.Http.HttpResults;
using Selections.API.Apis.Requests;
using Selections.API.Application.Commands;
using Selections.API.Application.Queries.ViewModels;

namespace Selections.API.Apis;

public static class SelectionsApi
{
    public static RouteGroupBuilder MapSelectionsApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/selections");
        
        api.MapGet("{selectionId:guid}", GetSelectionAsync);
        api.MapGet("/", GetSelectionsAsync);
        api.MapPost("/", CreateSelectionAsync);
        api.MapPut("/cancel", CancelSelectionAsync);

        return api;
    }

    public static async Task<Results<Ok<Selection>, NotFound>> GetSelectionAsync(
        Guid selectionId,
        [AsParameters] SelectionsServices services)
    {
        var selection = await services.Queries.GetAsync(selectionId);

        if (selection is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(selection);
    }

    public static async Task<Ok<List<Selection>>> GetSelectionsAsync(
        [AsParameters] SelectionsServices services)
    {
        var selections = await services.Queries.GetAsync();

        return TypedResults.Ok(selections);
    }
    
    public static async Task<Results<Ok, BadRequest<string>>> CreateSelectionAsync(
        CreateSelectionRequest request,
        [AsParameters] SelectionsServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            nameof(CreateSelectionRequest),
            nameof(request.UserId),
            request.UserId);

        var createSelectionCommand = new CreateSelectionCommand(
            request.Name,
            request.Items,
            request.UserId);
        
        var result = await services.Mediator.Send(createSelectionCommand);

        if (result)
        {
            services.Logger.LogInformation("CreateSelectionCommand succeeded");
        }
        else
        {
            services.Logger.LogWarning("CreateSelectionCommand failed");
        }

        return TypedResults.Ok();
    }
    
    public static async Task<Results<Ok, ProblemHttpResult>> CancelSelectionAsync(
        CancelSelectionCommand command,
        [AsParameters] SelectionsServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            nameof(CancelSelectionCommand),
            nameof(command.Id),
            command.Id);

        var commandResult = await services.Mediator.Send(command);

        if (!commandResult)
        {
            return TypedResults.Problem(detail: "Cancel selection failed to process.", statusCode: 500);
        }

        return TypedResults.Ok();
    }
}