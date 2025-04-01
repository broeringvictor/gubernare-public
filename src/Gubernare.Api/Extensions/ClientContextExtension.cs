
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Gubernare.Api.Extensions;

public static class ClientContextExtension
{

    public static void AddContractContext(this WebApplicationBuilder builder)
    {
        #region Create
        builder.Services.AddTransient<
            Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts.IRepository,
            Infrastructure.Contexts.ClientContext.UseCases.CreateContract.Repository>();
        #endregion
    }

    public static void AddIndividualClientContext(this WebApplicationBuilder builder)
    {
        #region Create
        builder.Services.AddTransient<
            Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Contracts.IRepository,
            Infrastructure.Contexts.ClientContext.UseCases.CreateIndividualClient.Repository>();
        #endregion
    }


    public static IEndpointRouteBuilder MapClientApiV1(this IEndpointRouteBuilder routes)
    {
        // Agrupa as rotas sob "/api/v1/clients"
        var api = routes.MapGroup("api/v1/clients")
                        .WithTags("Client Operations");

        // ROTA 1: "POST /api/v1/clients/contracts"
        api.MapPost("/contracts", CreateContract)
           .WithSummary("CreateContract")
           .WithDescription("Cria um novo contrato para o cliente")
           .WithOpenApi();

        // ROTA 2: "POST /api/v1/clients/individuals"
        api.MapPost("/individuals", CreateIndividualClient)
           .WithName("CreateIndividualClient")
           .WithSummary("CreateIndividualClient")
           .WithDescription("Cria um cliente pessoa física")
           .WithOpenApi();

        return routes;
    }

    // ===========================================================
    //  Rota: /api/v1/clients/contracts => CreateContract
    // ===========================================================
    public static async Task<Results<
        Created<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>,
        BadRequest<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>,
        NotFound<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>,
        StatusCodeHttpResult
    >>
    CreateContract(
        [FromBody] Domain.Contexts.ClientContext.UseCases.CreateContract.Request request,
        [FromServices] IRequestHandler<
            Domain.Contexts.ClientContext.UseCases.CreateContract.Request,
            Domain.Contexts.ClientContext.UseCases.CreateContract.Response> handler
    )
    {
        var result = await handler.Handle(request, CancellationToken.None);

        if (result.IsSuccess)
        {
            // Se deu certo, retornamos 201 com o payload
            return TypedResults.Created(
                $"/api/v1/clients/contracts/{result.Data?.Id}",
                result
            );
        }


        switch (result.Status)
        {
            case 400:
                return TypedResults.BadRequest(result);
            case 404:
                return TypedResults.NotFound(result);
            case 500:
                // Se precisar de 500, sem typed results para "500 + body":
                return (Results<Created<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>, BadRequest<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>, NotFound<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>, StatusCodeHttpResult>)Results.Json(result, statusCode: 500);

            default:
                return (Results<Created<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>, BadRequest<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>, NotFound<Domain.Contexts.ClientContext.UseCases.CreateContract.Response>, StatusCodeHttpResult>)Results.Json(result, statusCode: result.Status);
        }
    }

    // ===========================================================

public static async Task<Results<
    Created<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
    BadRequest<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
    NotFound<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
    StatusCodeHttpResult
>>
CreateIndividualClient(
    [FromBody] Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Request request,
    [FromServices] IRequestHandler<
        Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Request,
        Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response> handler
)
{
    var result = await handler.Handle(request, CancellationToken.None);

    if (result.IsSuccess)
    {
        // Retorna 201
        return TypedResults.Created(
            $"/api/v1/clients/individuals/{result.Data?.Id}",
            result
        );
    }

    // Se não houve sucesso, tratamos via switch:
    switch (result.Status)
    {
        case 400:
            return TypedResults.BadRequest(result);

        case 404:
            return TypedResults.NotFound(result);

        case 500:
            // Precisamos devolver JSON no corpo, mas não existe
            // "InternalServerError<T>" nativo. Então fazemos:
            return (Results<
                Created<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
                BadRequest<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
                NotFound<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
                StatusCodeHttpResult
            >)Results.Json(result, statusCode: 500);

        default:
            // Para status custom (418, 501, etc.)
            return (Results<
                Created<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
                BadRequest<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
                NotFound<Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response>,
                StatusCodeHttpResult
            >)Results.Json(result, statusCode: result.Status);
    }
}

}
