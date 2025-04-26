using Flunt.Notifications;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Update;

public sealed class Response : SharedContext.UseCases.Response
{
    private Response() { }

    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message       = message;
        Status        = status;
        Notifications = notifications;
    }

    public Response(string message, ResponseData data)
    {
        Message = message;
        Status  = 200;
        Data    = data;
    }

    public ResponseData? Data { get; init; }
}

public sealed record ResponseData(Guid Id, string Title, bool IsCompleted);