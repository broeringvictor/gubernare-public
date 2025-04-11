using Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Contracts;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;  // Necessário para usar ToListAsync e IQueryable

namespace Gubernare.Infrastructure.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin
{
    public class Repository(AppDbContext context) : IRepository
    {
        // Construtor para injeção de dependência

        public async Task<List<Domain.Contexts.AccountContext.Entities.CourtLogin>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
        {
            // Busca todos os CourtLogins do usuário
            var response = await context.CourtLogins
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);

            return response;
        }
    }
}