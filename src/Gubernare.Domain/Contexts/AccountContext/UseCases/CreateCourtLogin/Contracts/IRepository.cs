using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.Entities;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CreateCourtLogin.Contracts;

public interface IRepository
{
    Task SaveAsync(CourtLogin courtLogin, CancellationToken cancellationToken);
}
