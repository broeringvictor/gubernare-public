
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.Delete;

public sealed class Repository(AppDbContext context) : IRepository
{

    public async Task<int> DeleteAsync(Guid id, CancellationToken ct)
    {
        return await context.ToDos
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync(ct);   
    }
}