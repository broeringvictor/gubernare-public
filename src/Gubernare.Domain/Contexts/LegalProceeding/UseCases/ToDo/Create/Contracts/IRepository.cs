namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Create.Contracts;

public interface IRepository
{
    Task SaveAsync(Entities.ToDo todo, CancellationToken cancellationToken);
}