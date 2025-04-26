using Flunt.Notifications;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete;

public sealed class Response : SharedContext.UseCases.Response
{
    private Response() { }

    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message       = message;
        Status        = status;
        Notifications = notifications;
    }

    public Response(string message, IEnumerable<ResponseItem> items)
    {
        Message = message;
        Status  = 200;
        Items   = items;
    }

    public IEnumerable<ResponseItem>? Items { get; init; }
}

public sealed record ResponseItem(
    Guid      Id,
    string?    Title,
    string?   Description,
    DateTime  CreatedAt,
    DateTime? DueDate,
    bool      IsCompleted,
    Guid?      ProcessId
);