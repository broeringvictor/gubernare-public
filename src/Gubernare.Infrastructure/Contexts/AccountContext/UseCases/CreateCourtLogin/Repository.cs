using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Contracts;
using Gubernare.Infrastructure.Data;

namespace Gubernare.Infrastructure.Contexts.AccountContext.UseCases.CreateCourtLogin;

public class Repository(AppDbContext context) : IRepository
{
    public async Task SaveAsync(Domain.Contexts.AccountContext.Entities.CourtLogin courtLogin, CancellationToken cancellationToken)
    {
        await context.CourtLogins.AddAsync(courtLogin, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
       
    }
}