using Gubernare.Domain.Contexts.AccountContext.Entities;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts;

public interface IRepository
{
    public Task<CourtLogin?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    
}
