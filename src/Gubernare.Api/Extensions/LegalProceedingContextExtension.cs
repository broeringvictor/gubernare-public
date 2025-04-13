using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Gubernare.Api.Extensions;

public static class LegalProceedingContextExtension
{
    public static void AddLegalProceedingContext(this WebApplicationBuilder builder)
    {
        AddSearchAllLegalProceedingServices(builder);

    }

    private static void AddSearchAllLegalProceedingServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts.IRepository,
            Infrastructure.Contexts.LegalProeeding.UseCases.SearchAllLegalProceeding.Repository>();

        builder.Services.AddTransient<
            Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts.IService,
            Infrastructure.Contexts.LegalProeeding.UseCases.SearchAllLegalProceeding.Service>();
    }

   
    public static IEndpointRouteBuilder MapLegalProceedingApiV1(this IEndpointRouteBuilder routes)
    {
        var api = routes.MapGroup("api/v1/search")
                        .WithTags("Account Management")
                        .WithOpenApi();

       
        api.MapGet("/proceedings", SearchAllLegalProceeding)
            .RequireAuthorization()
            .WithSummary("SearchAllLegalProceeding")
            .WithDescription("Procura por todos os processos em um Tribunal")
            .ProducesValidationProblem();

        return routes;
    }
    private static async
        Task<Results<Ok<Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Response>, ProblemHttpResult>>
        SearchAllLegalProceeding(
            [FromBody] Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Request request,
            ClaimsPrincipal user,
            [FromServices] IRequestHandler<Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Request,
                Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Response> handler)
    {
        var result = await handler.Handle(request, CancellationToken.None);

        if (!result.IsSuccess)
            return TypedResults.Problem(result.ToProblemDetails());


        return TypedResults.Ok(result);
    }
    

   
}
