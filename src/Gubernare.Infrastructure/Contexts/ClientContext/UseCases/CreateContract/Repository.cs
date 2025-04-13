using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Contexts.ClientContext.UseCases.CreateContract;

public class Repository(AppDbContext context) : IRepository
{
    public async Task SaveAsync(Contract contract, CancellationToken cancellationToken)
    {

        await context.Contracts.AddAsync(contract, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
