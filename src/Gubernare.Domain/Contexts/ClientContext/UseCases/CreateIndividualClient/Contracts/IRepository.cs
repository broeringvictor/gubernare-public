

using Gubernare.Domain.Contexts.ClientContext.Entities;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string cpf, CancellationToken cancellationToken);
    Task SaveAsync(IndividualClient individualClient, CancellationToken cancellationToken);
}
