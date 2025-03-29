using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Contexts.AccountContext.UseCases.Authenticate;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) =>
        await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
}