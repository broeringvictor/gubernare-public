using MediatR;

namespace Gubernare.Api.Extensions;

public static class ClientContextExtension
{
    public static void AddContractContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts.IRepository,
            Gubernare.Infrastructure.Contexts.ClientContext.UseCases.CreateContract.Repository>();

        #endregion

        
    }

    public static void MapContractEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/contracts", async (
            Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Request request,
            IRequestHandler<
                Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Request,
                Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created($"api/v1/contracts/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        });

        #endregion

        
    }
}
