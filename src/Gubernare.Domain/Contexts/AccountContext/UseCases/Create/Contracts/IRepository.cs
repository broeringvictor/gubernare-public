using Gubernare.Domain.Contexts.AccountContext.Entities;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}