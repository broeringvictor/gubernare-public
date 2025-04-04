using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Gubernare.Domain.Contexts.AccountContext.UseCases.Create;
using Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate;

namespace Gubernare.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        AddCreateAccountServices(builder);
        AddAuthenticateServices(builder);
    }

    private static void AddCreateAccountServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            Domain.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            Infrastructure.Contexts.AccountContext.UseCases.Create.Repository>();

        builder.Services.AddTransient<
            Domain.Contexts.AccountContext.UseCases.Create.Contracts.IService,
            Infrastructure.Contexts.AccountContext.UseCases.Create.Service>();
    }

    private static void AddAuthenticateServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
            Infrastructure.Contexts.AccountContext.UseCases.Authenticate.Repository>();
    }

    public static IEndpointRouteBuilder MapAccountApiV1(this IEndpointRouteBuilder routes)
    {
        var api = routes.MapGroup("api/v1/account")
                        .WithTags("Account Management")
                        .WithOpenApi();

        api.MapPost("/users", CreateUser)
            .WithSummary("Cria novo usuário")
            .WithDescription("Cadastra um novo usuário no sistema")
            .ProducesProblem(400)
            .ProducesProblem(500);

        api.MapPost("/authenticate", AuthenticateUser)
            .WithSummary("Autenticação")
            .WithDescription("Gera token JWT para acesso ao sistema")
            .ProducesProblem(401)
            .ProducesProblem(500);

        api.MapGet("/authenticated", IsAuthenticated)
           .RequireAuthorization()
           .WithSummary("Verifica autenticação")
           .WithDescription("Valida se o usuário está autenticado")
           .ProducesValidationProblem();

        return routes;
    }

    private static async Task<Results<Created<Domain.Contexts.AccountContext.UseCases.Create.Response>, ProblemHttpResult>> CreateUser(
        [FromBody] Domain.Contexts.AccountContext.UseCases.Create.Request request,
        [FromServices] IRequestHandler<Domain.Contexts.AccountContext.UseCases.Create.Request, Domain.Contexts.AccountContext.UseCases.Create.Response> handler)
    {
        var result = await handler.Handle(request, CancellationToken.None);

        return result.IsSuccess
            ? TypedResults.Created($"/api/v1/account/users/{result.Data?.Id}", result)
            : TypedResults.Problem(result.ToProblemDetails());
    }

    private static async Task<Results<Ok<Domain.Contexts.AccountContext.UseCases.Authenticate.Response>, ProblemHttpResult>> AuthenticateUser(
        [FromBody] Domain.Contexts.AccountContext.UseCases.Authenticate.Request request,
        [FromServices] IRequestHandler<Domain.Contexts.AccountContext.UseCases.Authenticate.Request, Domain.Contexts.AccountContext.UseCases.Authenticate.Response> handler,
        [FromServices] IConfiguration config)
    {
        var result = await handler.Handle(request, CancellationToken.None);

        if (!result.IsSuccess)
            return TypedResults.Problem(result.ToProblemDetails());

        result.Data!.Token = JwtExtension.Generate(result.Data);
        return TypedResults.Ok(result);
    }

    private static IResult IsAuthenticated()
    {
        return TypedResults.Ok(new { Message = "Autenticação válida" });
    }
}
