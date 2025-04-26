using Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.UseCases.ToDo.GetAll;

public sealed class Repository(AppDbContext context) : IRepository
{
    public async Task<IReadOnlyList<Domain.Contexts.LegalProceeding.Entities.ToDo>> GetAllByUserAsync(Guid userId, CancellationToken ct) =>
        await context.Set<Domain.Contexts.LegalProceeding.Entities.ToDo>()
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);
}