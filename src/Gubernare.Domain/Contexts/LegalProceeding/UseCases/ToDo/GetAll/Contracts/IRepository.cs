using Gubernare.Domain.Contexts.LegalProceeding.Entities;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.GetAll.Contracts;

public interface IRepository
{
    Task<IReadOnlyList<Entities.ToDo>> GetAllByUserAsync(Guid userId, CancellationToken ct);
}