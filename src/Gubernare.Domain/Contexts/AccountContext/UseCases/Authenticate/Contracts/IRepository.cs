using Gubernare.Domain.Contexts.AccountContext.Entities;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}