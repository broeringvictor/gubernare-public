using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Contexts.ClientContext.UseCases.CreateContract;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
        => _context = context;

    public async Task SaveAsync(Contract contract, CancellationToken cancellationToken)
    {

        await _context.Contracts.AddAsync(contract, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
