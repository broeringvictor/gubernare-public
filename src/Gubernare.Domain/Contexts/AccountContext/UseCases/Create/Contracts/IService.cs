using Gubernare.Domain.Contexts.AccountContext.Entities;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}