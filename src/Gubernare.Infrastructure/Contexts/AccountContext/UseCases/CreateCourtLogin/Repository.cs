using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.AccountContext.UseCases.CreateCourtLogin.Contracts;
using Gubernare.Infrastructure.Data;

namespace Gubernare.Infrastructure.Contexts.AccountContext.UseCases.CreateCourtLogin;

public class Repository(AppDbContext context) : IRepository
{
    public async Task SaveAsync(CourtLogin courtLogin, CancellationToken cancellationToken)
    {
        await context.CourtLogins.AddAsync(courtLogin, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
       
    }
}