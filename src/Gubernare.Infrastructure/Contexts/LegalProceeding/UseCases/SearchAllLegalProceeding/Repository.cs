using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<CourtLogin?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        var response = await context.CourtLogins
            .FirstOrDefaultAsync(x => x.UserId == Id, cancellationToken);

        return response;
    }

}