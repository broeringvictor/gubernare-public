namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete.Contracts;

public interface IRepository
{
    Task<int> DeleteAsync(Guid id, CancellationToken ct);
}