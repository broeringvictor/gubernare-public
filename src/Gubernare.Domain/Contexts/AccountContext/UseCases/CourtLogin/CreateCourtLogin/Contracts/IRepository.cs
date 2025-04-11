namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin.Contracts;

public interface IRepository
{
    Task SaveAsync(Entities.CourtLogin courtLogin, CancellationToken cancellationToken);
}
