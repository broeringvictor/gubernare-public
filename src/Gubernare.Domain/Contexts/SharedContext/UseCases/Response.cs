using Flunt.Notifications;

namespace Gubernare.Domain.Contexts.SharedContext.UseCases;

public abstract class Response
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; }
    public bool IsSuccess => Status is >= 200 and <= 299;
    public IEnumerable<Notification>? Notifications { get; set; }
}
