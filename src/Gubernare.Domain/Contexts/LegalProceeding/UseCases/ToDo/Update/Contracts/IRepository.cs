using Gubernare.Domain.Contexts.LegalProceeding.Entities;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Update.Contracts;

public interface IRepository
{
    Task<Entities.ToDo?> GetByIdAsync(Guid? id, CancellationToken ct);
    Task UpdateAsync(Entities.ToDo todo, CancellationToken ct);
}