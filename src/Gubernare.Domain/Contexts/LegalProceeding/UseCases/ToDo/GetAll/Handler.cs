using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Contracts;
using MediatR;
using System.Linq;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll;

public sealed class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken ct)
    {
        
        var todos = await _repository.GetAllByUserAsync(request.UserId, ct);

     var items = todos.Select(t => new ResponseItem(
            t.Id, t.Title, t.Description, t.CreatedAt, t.DueDate,
            t.IsCompleted, t.ProcessId));

        return new Response("Tarefas carregadas com sucesso", items);
    }
}