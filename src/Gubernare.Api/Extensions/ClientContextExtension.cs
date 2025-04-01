using MediatR;

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

    public static void MapContractEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/clients/contracts", async (
            Domain.Contexts.ClientContext.UseCases.CreateContract.Request request,
            IRequestHandler<
                Domain.Contexts.ClientContext.UseCases.CreateContract.Request,
                Domain.Contexts.ClientContext.UseCases.CreateContract.Response> handler) =>
        {
            var result = await handler.Handle(request, CancellationToken.None);
            return result.IsSuccess
                ? Results.Created($"api/v1/clients/contracts/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        });

        #endregion
    }

    public static void MapIndividualClientEndpoints(this WebApplication app)
        {
            #region Create

            app.MapPost("api/v1/clients/individuals", async (
                Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Request request,
                IRequestHandler<
                    Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Request,
                    Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Response> handler) =>
            {
                var result = await handler.Handle(request, CancellationToken.None);
                return result.IsSuccess
                    ? Results.Created($"api/v1/clients/individuals/{result.Data?.Id}", result)
                    : Results.Json(result, statusCode: result.Status);
            });

            #endregion




    }
}
