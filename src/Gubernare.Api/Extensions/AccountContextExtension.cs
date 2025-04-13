using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


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
        builder.Services.AddTransient<
            Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Contracts.IRepository,
            Infrastructure.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Repository>();
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

        api.MapGet("/authenticat", IsAuthenticated)
           .RequireAuthorization()
           .WithSummary("Verifica autenticação")
           .WithDescription("Valida se o usuário está autenticado")
           .ProducesValidationProblem();
        
        api.MapPost("/courts", CreateCourtLogin)
            .RequireAuthorization()
            .WithSummary("Cadastra um tribunal ao sistema")
            .WithDescription("Cadastra um novo tribunal no sistema")
            .ProducesValidationProblem();
        
        api.MapGet("/courts", GetAllCourtLogin)
            .RequireAuthorization()
            .WithSummary("Pegar todos os tribunais cadastrados")
            .WithDescription("Retorna uma lista com o ID e o Nome dos tribunais cadastrados")
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
        Task<Results<Ok<Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Response>, ProblemHttpResult>>
        CreateCourtLogin(
            [FromBody] Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Request request,
            ClaimsPrincipal user,
            [FromServices] IRequestHandler<Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Request,
                Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Response> handler)
    {
        request = request with { UserId = new Guid(user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value!) };
        request = request with { UserId = Guid.Parse(user.Id()) };
        

        var result = await handler.Handle(request, CancellationToken.None);

        if (!result.IsSuccess)
            return TypedResults.Problem(result.ToProblemDetails());


        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<
        Ok<Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Response>, 
        ProblemHttpResult
    >> GetAllCourtLogin(
        ClaimsPrincipal user,
        [FromServices] IRequestHandler<
            Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Request,
            Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Response
        > handler)
    {
        var userId = Guid.Parse(user.Id()); // ou extraia do claim se preferir
        var request = new Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Request(userId);

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
