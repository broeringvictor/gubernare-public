using Gubernare.Domain.Contexts.AccountContext.UseCases.Create;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Gubernare.Api.Extensions
{
    public static class AccountContextExtension
    {
        // Aqui permanece a configuração de DI, como estava
        public static void AddAccountContext(this WebApplicationBuilder builder)
        {
            #region Create
            builder.Services.AddTransient<
                Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
                Gubernare.Infrastructure.Contexts.AccountContext.UseCases.Create.Repository>();

            builder.Services.AddTransient<
                Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Contracts.IService,
                Gubernare.Infrastructure.Contexts.AccountContext.UseCases.Create.Service>();
            #endregion

            #region Authenticate
            builder.Services.AddTransient<
                Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
                Gubernare.Infrastructure.Contexts.AccountContext.UseCases.Authenticate.Repository>();
            #endregion
        }


    public static IEndpointRouteBuilder MapAccountApiV1(this IEndpointRouteBuilder routes)
    {
        var api = routes.MapGroup("api/v1")
                        .WithTags("Account Operations");

        api.MapPost("/users", CreateUser)
            .WithSummary("CreateUser")
            .WithDescription("Cria um novo usuário cliente")
            .WithOpenApi();

        api.MapPost("/authenticate", AuthenticateUser)
            .WithSummary("Authenticate")
            .WithDescription("Loga no sistema.")
            .WithOpenApi();

        api.MapGet("/authenticated", IsAuthenticated)
           .RequireAuthorization()
           .WithSummary("IsAuthenticated")
           .WithOpenApi();

        return routes;
    }

    public static async Task<Results<
        Created<Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Response>,
        BadRequest<Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Response>,
        NotFound<Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Response>,
        StatusCodeHttpResult
    >>
    CreateUser(
        [FromBody] Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Request request,
        [FromServices] IRequestHandler<
            Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Request,
            Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Response> handler)
    {
        var result = await handler.Handle(request, CancellationToken.None);

        if (result.IsSuccess)
        {
            return TypedResults.Created($"/api/v1/users/{result.Data?.Id}", result);
        }

        switch (result.Status)
        {
            case 400: return TypedResults.BadRequest(result);
            case 404: return TypedResults.NotFound(result);

            // Ao invés de "TypedResults.StatusCode(500, result)", use:
            case 500:
                return (Results<Created<Response>, BadRequest<Response>, NotFound<Response>, StatusCodeHttpResult>)Results.Json(result);

            default:
                return (Results<Created<Response>, BadRequest<Response>, NotFound<Response>, StatusCodeHttpResult>)Results.Json(result, statusCode: result.Status);
        }
    }

    public static async Task<Results<
        Ok<Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Response>,
        BadRequest<Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Response>,
        NotFound<Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Response>,
        StatusCodeHttpResult
    >>
    AuthenticateUser(
        [FromBody] Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Request request,
        [FromServices] IRequestHandler<
            Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Request,
            Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Response> handler)
    {
        var result = await handler.Handle(request, CancellationToken.None);

        if (!result.IsSuccess)
        {
            return (Results<Ok<Domain.Contexts.AccountContext.UseCases.Authenticate.Response>, BadRequest<Domain.Contexts.AccountContext.UseCases.Authenticate.Response>, NotFound<Domain.Contexts.AccountContext.UseCases.Authenticate.Response>, StatusCodeHttpResult>)Results.Json(result, statusCode: 500);


        }
        result.Data.Token = JwtExtension.Generate(result.Data);
        return TypedResults.Ok(result);
    }

    // Observe a assinatura mudando para IResult
    public static IResult IsAuthenticated(HttpContext context)
    {
        return TypedResults.Ok(new { Message = "Você está autorizado" });
    }
}
}
