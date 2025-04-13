using Flunt.Notifications;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding;

public class Response : SharedContext.UseCases.Response
{
    protected Response()
    {
    }

    public Response(
        string message, 
        int status, 
        IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }
    
    // Construtor que recebe lista de ResponseData
    public Response(string message, IEnumerable<ResponseData> data)
    {
        Message = message;
        Status = 201;
        Notifications = null;
        Data = data; // Data agora é uma lista
    }

    // Agora a propriedade é uma lista
    public IEnumerable<ResponseData>? Data { get; set; }
}