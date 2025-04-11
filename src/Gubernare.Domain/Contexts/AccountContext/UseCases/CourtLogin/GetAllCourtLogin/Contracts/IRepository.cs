namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin.Contracts;

public interface IRepository
{
    public Task<List<Entities.CourtLogin>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
}
