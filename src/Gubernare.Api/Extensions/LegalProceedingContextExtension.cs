using System.Security.Claims;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Create;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Update;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete;
using Gubernare.Infrastructure.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Gubernare.Api.Extensions;

public static class LegalProceedingContextExtension
{
    // --------------------------------------------------------------------- //
    //  DEPENDENCY-INJECTION                                                 //
    // --------------------------------------------------------------------- //
    public static void AddLegalProceedingContext(this WebApplicationBuilder builder)
    {
        AddSearchAllLegalProceedingServices(builder);
        AddToDoServices(builder);
    }

    private static void AddSearchAllLegalProceedingServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts.IRepository,
            Repository>();

        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts.IService,
            Service>();
    }

    private static void AddToDoServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.ToDo.Create.Contracts.IRepository,
            Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.Create.Repository>();

        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Contracts.IRepository,
            Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Repository>();

        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.ToDo.Update.Contracts.IRepository,
            Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.Update.Repository>();

        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete.Contracts.IRepository,
            Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.Complete.Repository>();
        
        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete.Contracts.IRepository,
            Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.Delete.Repository>();
    }

    // --------------------------------------------------------------------- //
    //  ENDPOINTS                                                            //
    // --------------------------------------------------------------------- //
    public static IEndpointRouteBuilder MapLegalProceedingApiV1(this IEndpointRouteBuilder routes)
    {
        var api = routes.MapGroup("api/v1")
                        .WithTags("Legal Proceeding")
                        .WithOpenApi();

        // -------------------- Existing search endpoint -------------------- //
        api.MapPost("/proceedings", SearchAllLegalProceeding)
            .RequireAuthorization()
            .WithSummary("SearchAllLegalProceeding")
            .WithDescription("Procura por todos os processos em um Tribunal")
            .ProducesValidationProblem();

        // --------------------------- ToDo --------------------------------- //
        api.MapPost("/todos", CreateToDo)
           .RequireAuthorization()
           .WithSummary("Create ToDo")
           .WithDescription("Cria uma nova tarefa para o usuário logado.")
           .ProducesValidationProblem();

        api.MapGet("/todos", GetAllToDo)
           .RequireAuthorization()
           .WithSummary("GetAll ToDo")
           .WithDescription("Lista todas as tarefas do usuário logado.")
           .ProducesValidationProblem();

        api.MapPatch("/todos/{id:guid}", UpdateToDo)
           .RequireAuthorization()
           .WithSummary("Update ToDo")
           .WithDescription("Atualiza os campos de uma tarefa existente.")
           .ProducesValidationProblem();

        api.MapPatch("/todos/{id:guid}/done", CompleteToDo)
           .RequireAuthorization()
           .WithSummary("Toggle complete")
           .WithDescription("Alterna o estado de conclusão da tarefa.")
           .ProducesValidationProblem();
        
        api.MapDelete("/todos/{id:guid}", DeleteToDo)
            .RequireAuthorization()
            .WithSummary("Delete ToDo")
            .WithDescription("Exclui uma tarefa do usuário logado.")
            .ProducesValidationProblem();

        return routes;
    }

    // --------------------------------------------------------------------- //
    //  HANDLERS (Minimal-API wrappers)                                      //
    // --------------------------------------------------------------------- //

    // -------- Search all proceedings ------------------------------------- //
    private static async Task<Results<
        Ok<Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Response>,
        ProblemHttpResult>>
        SearchAllLegalProceeding(
            [FromBody] Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Request request,
            ClaimsPrincipal user,
            [FromServices] IRequestHandler<
                Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Request,
                Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Response> handler)
    {
        // Coloca o Id do usuário vindo dos claims
        request = request with { Id = Guid.Parse(user.Id()) };

        var result = await handler.Handle(request, CancellationToken.None);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.Problem(result.ToProblemDetails());
    }

    // --------------------------- CREATE ---------------------------------- //
    private static async Task<Results<Created<Domain.Contexts.LegalProceeding.UseCases.ToDo.Create.Response>, ProblemHttpResult>> CreateToDo(
        [FromBody] Domain.Contexts.LegalProceeding.UseCases.ToDo.Create.Request request,
        ClaimsPrincipal user,
        [FromServices] IRequestHandler<Domain.Contexts.LegalProceeding.UseCases.ToDo.Create.Request, Domain.Contexts.LegalProceeding.UseCases.ToDo.Create.Response> handler)
    {
        request = request with { UserId = new Guid(user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value!) };
        request = request with { UserId = Guid.Parse(user.Id()) };

        var result = await handler.Handle(request, CancellationToken.None);

        return result.IsSuccess
            ? TypedResults.Created($"/api/v1/todos/{result.Data!.Id}", result)
            : TypedResults.Problem(result.ToProblemDetails());
    }

    // --------------------------- GET ALL ---------------------------------- //
    private static async Task<Results<Ok<Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Response>, ProblemHttpResult>> GetAllToDo(
        ClaimsPrincipal user,
        [FromServices] IRequestHandler<Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Request, Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Response> handler)
    {
        var request = new Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Request(Guid.Parse(user.Id()));
        var result  = await handler.Handle(request, CancellationToken.None);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.Problem(result.ToProblemDetails());
    }

    // --------------------------- UPDATE ----------------------------------- //
    private static async Task<Results<Ok<Domain.Contexts.LegalProceeding.UseCases.ToDo.Update.Response>, ProblemHttpResult>> UpdateToDo(
        [FromRoute] Guid id,
        [FromBody]  Domain.Contexts.LegalProceeding.UseCases.ToDo.Update.Request body,
        [FromServices] IRequestHandler<Domain.Contexts.LegalProceeding.UseCases.ToDo.Update.Request, Domain.Contexts.LegalProceeding.UseCases.ToDo.Update.Response> handler)
    {
        var request = body with { Id = id };
        var result  = await handler.Handle(request, CancellationToken.None);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.Problem(result.ToProblemDetails());
    }

    // --------------------------- COMPLETE / TOGGLE ------------------------ //
    private static async Task<Results<Ok<Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete.Response>, ProblemHttpResult>> CompleteToDo(
        [FromRoute] Guid id,
        [FromServices] IRequestHandler<Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete.Request, Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete.Response> handler)
    {
        var request = new Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete.Request(id);
        var result  = await handler.Handle(request, CancellationToken.None);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.Problem(result.ToProblemDetails());
    }
    // --------------------------- DELETE HANDLER --------------------------- //
    private static async Task<Results<
            Ok<Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete.Response>,
            ProblemHttpResult>>
        DeleteToDo(
            [FromRoute] Guid id,
            [FromServices] IRequestHandler<
                Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete.Request,
                Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete.Response> handler)
    {
        var request = new Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete.Request(id);
        var result  = await handler.Handle(request, CancellationToken.None);

        return result.IsSuccess
            ? TypedResults.Ok(result)          // 200 OK com corpo padrão de Response
            : TypedResults.Problem(result.ToProblemDetails());
    }

}
