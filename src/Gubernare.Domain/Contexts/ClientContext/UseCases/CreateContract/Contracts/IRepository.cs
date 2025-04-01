using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.Entities;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts;

public interface IRepository
{
    Task SaveAsync(Contract contract, CancellationToken cancellationToken);
}
