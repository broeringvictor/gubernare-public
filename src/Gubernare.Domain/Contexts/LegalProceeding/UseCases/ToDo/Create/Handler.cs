using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Create.Contracts;
using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Create;

public sealed class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken ct)
    {
        // 01. Validação
        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        // 02. Instancia o agregado
        Entities.ToDo todo;
        
        try
        {
            todo = new Entities.ToDo
            {
                Title       = request.Title,
                Description = request.Description,
                DueDate     = request.DueDate,
                ProcessId   = request.ProcessId,
                UserId      = request.UserId,
                CreatedAt   = DateTime.Now,
                IsCompleted = false
            };
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        // 03. Persiste
        try
        {
            await _repository.SaveAsync(todo, ct);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        return new Response(
            "Tarefa criada com sucesso",
            new ResponseData(todo.Id, todo.Title));
    }
}
