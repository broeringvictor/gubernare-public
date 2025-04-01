
using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Contexts.ClientContext.UseCases.CreateIndividualClient;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
        => _context = context;

    public async Task<bool> AnyAsync(string cpf, CancellationToken cancellationToken)
        => await _context
            .IndividualClients
            .AsNoTracking()
            .AnyAsync(x => x.CpfNumber.CpfValue == cpf, cancellationToken: cancellationToken);

    public async Task SaveAsync(IndividualClient individualClient, CancellationToken cancellationToken)
    {
        await _context.IndividualClients.AddAsync(individualClient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
