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
    public Response(string message, ResponseData data)
    {
        Message = message;
        Status = 200; // 200 é mais adequado para respostas de sucesso
        Data = data;
    }

    // Agora a propriedade é uma lista
    public ResponseData? Data { get; set; }
}