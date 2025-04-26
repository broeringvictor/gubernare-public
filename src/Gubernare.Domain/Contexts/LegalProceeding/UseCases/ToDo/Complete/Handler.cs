using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete.Contracts;
using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete;

public sealed class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken ct)
    {
        // Recupera a entidade
        var todo = await _repository.GetByIdAsync(request.Id, ct);
        if (todo is null)
            return new Response("[ERCMP01] Tarefa não encontrada", 404);

        // Inverte o status
        todo.IsCompleted = !todo.IsCompleted;

        // Persiste
        try
        {
            await _repository.UpdateAsync(todo, ct);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        var msg = todo.IsCompleted
            ? "ToDo marcado como concluído"
            : "ToDo marcado como não concluído";

        return new Response(
            msg,
            new ResponseData(todo.Id, todo.Title, todo.IsCompleted));
    }
}