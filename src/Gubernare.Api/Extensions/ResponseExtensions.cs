using Microsoft.AspNetCore.Mvc;


namespace Gubernare.Api.Extensions;

public static class ResponseExtensions
{
    public static ProblemDetails ToProblemDetails(this Domain.Contexts.SharedContext.UseCases.Response response)
    {
        return new ProblemDetails
        {
            Title = "Operação Inválida",
            Detail = response.Message,
            Status = response.Status,
            Extensions = { ["errors"] = response.Notifications }
        };
    }
}
