using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete.Contracts;
using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete;

public sealed class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken ct)
    {
        int rows;
        try
        {
            // bulk-delete: retorna nº de linhas excluídas
            rows = await _repository.DeleteAsync(request.Id, ct);
        }
        catch (Exception ex)
        {
            // log (opcional) e devolve erro padronizado
            return new Response($"Falha ao excluir tarefa: {ex.Message}", 500);
        }

        if (rows == 0)
            return new Response("Tarefa não encontrada", 404);

        return new Response("Tarefa excluída com sucesso", 204); 
    }
}