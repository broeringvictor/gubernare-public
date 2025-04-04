using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Gubernare.Api.Extensions;

namespace Gubernare.Api.Extensions;

public static class ClientContextExtension
{
    public static void AddClientContext(this WebApplicationBuilder builder)
    {
        AddContractContext(builder);
        AddIndividualClientContext(builder);
    }

    private static void AddContractContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts.IRepository,
            Infrastructure.Contexts.ClientContext.UseCases.CreateContract.Repository>();
    }

    private static void AddIndividualClientContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Contracts.IRepository,
            Infrastructure.Contexts.ClientContext.UseCases.CreateIndividualClient.Repository>();
    }

    public static IEndpointRouteBuilder MapClientApiV1(this IEndpointRouteBuilder routes)
    {
        var api = routes.MapGroup("api/v1/clients")
                        .WithTags("Client Operations")
                        .WithOpenApi();

        api.MapPost("/contracts", CreateContract)
           .WithSummary("CreateContract")
           .WithDescription("Cria um novo contrato para o cliente")
           .ProducesProblem(400)
           .ProducesProblem(500);

        api.MapPost("/individuals", CreateIndividualClient)
           .WithName("CreateIndividualClient")
           .WithSummary("Cria cliente pessoa física")
           .WithDescription("Cadastro completo com documentos e informações pessoais")
           .ProducesProblem(400)
           .ProducesProblem(500);

        return routes;
    }

    private static async Task<Results<Created<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>, ProblemHttpResult>>
        CreateContract(
            [FromBody] Domain.Contexts.ClientContext.UseCases.CreateContract.Request request,
            [FromServices] IRequestHandler<
                Domain.Contexts.ClientContext.UseCases.CreateContract.Request,
                Domain.Contexts.ClientContext.UseCases.CreateContract.Response> handler)
    {
        var result = await handler.Handle(request, CancellationToken.None);
        return result.IsSuccess
            ? TypedResults.Created($"/api/v1/clients/contracts/{result.Data?.Id}", result)
            : TypedResults.Problem(result.ToProblemDetails());
    }

    private static async Task<Results<Created<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>, ProblemHttpResult>>
        CreateIndividualClient(
            [FromBody] Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Request request,
            [FromServices] IRequestHandler<
                Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Request,
                Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response> handler)
    {
        var result = await handler.Handle(request, CancellationToken.None);
        return result.IsSuccess
            ? TypedResults.Created($"/api/v1/clients/individuals/{result.Data?.Id}", result)
            : TypedResults.Problem(result.ToProblemDetails());
    }
}
