using System.Security.Claims;
using Gubernare.Domain.Contexts.AccountContext.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Gubernare.Domain.Contexts.AccountContext.UseCases.Create;
using Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate;
using Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Contracts;
using Microsoft.AspNetCore.Authorization;


namespace Gubernare.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        AddCreateAccountServices(builder);
        AddAuthenticateServices(builder);
        AddCreateCourtLogin(builder);
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
    
    private static void AddCreateCourtLogin(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Contracts.IRepository,
            Infrastructure.Contexts.AccountContext.UseCases.CreateCourtLogin.Repository>();
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
        
        api.MapPost("/courts", CreateCourtLogin)
            .RequireAuthorization()
            .WithSummary("Cadastra um tribunal ao sistema")
            .WithDescription("Cadastra um novo tribunal no sistema")
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


    private static async
        Task<Results<Ok<Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Response>, ProblemHttpResult>>
        CreateCourtLogin(
            [FromBody] Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Request request,
            ClaimsPrincipal user,
            [FromServices] IRequestHandler<Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Request,
                Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Response> handler)
    {
        foreach (var claim in user.Claims)
        {
            Console.WriteLine($"{claim.Type} = {claim.Value}");
        }
        request = request with { UserId = new Guid(user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value!) };
        request = request with { UserId = Guid.Parse(user.Id()) };
        

        var result = await handler.Handle(request, CancellationToken.None);

        if (!result.IsSuccess)
            return TypedResults.Problem(result.ToProblemDetails());


        return TypedResults.Ok(result);
    }

    private static IResult IsAuthenticated()
    {
        return TypedResults.Ok(new { Message = "Autenticação válida" });
    }
}
