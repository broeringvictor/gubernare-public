using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Update.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.Update;

public sealed class Repository(AppDbContext context) : IRepository
{
    public async Task<Domain.Contexts.LegalProceeding.Entities.ToDo?> GetByIdAsync(Guid? id, CancellationToken ct) =>
        await context.ToDos
            .FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task UpdateAsync(Domain.Contexts.LegalProceeding.Entities.ToDo todo, CancellationToken ct)
    {
        context.Set<Domain.Contexts.LegalProceeding.Entities.ToDo>().Update(todo);
        await context.SaveChangesAsync(ct);
    }
}