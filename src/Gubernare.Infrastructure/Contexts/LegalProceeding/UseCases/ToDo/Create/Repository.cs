using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Create.Contracts;
using Gubernare.Infrastructure.Data;

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.Create;

public sealed class Repository(AppDbContext context) : IRepository
{
    public async Task SaveAsync(Domain.Contexts.LegalProceeding.Entities.ToDo todo, CancellationToken ct)
    {
        await context.ToDos.AddAsync(todo, ct);
        await context.SaveChangesAsync(ct);
    }


}