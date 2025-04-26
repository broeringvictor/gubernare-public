using Gubernare.Domain.Contexts.LegalProceeding.Entities;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Update.Contracts;
using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Update;

public sealed class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken ct)
    {
        // 01. Validação da requisição
        var spec = Specification.Ensure(request);
        if (!spec.IsValid)
            return new Response("Requisição inválida", 400, spec.Notifications);

        
        var todo = await _repository.GetByIdAsync(request.Id, ct);
        if (todo is null)
            return new Response("ToDo não encontrado", 404);

        // 03. Aplica alterações
        if (request.Title       is not null) todo.Title       = request.Title;
        if (request.Description is not null) todo.Description = request.Description;
        if (request.DueDate     is not null) todo.DueDate     = request.DueDate;
        if (request.IsCompleted is not null) todo.IsCompleted = request.IsCompleted.Value;

        // 04. Persiste
        try
        {
            await _repository.UpdateAsync(todo, ct);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        return new Response(
            "Tarefa atualizada com sucesso",
            new ResponseData(todo.Id, todo.Title, todo.IsCompleted));
    }
}