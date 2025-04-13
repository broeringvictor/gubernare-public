using Gubernare.Domain.Contexts.AccountContext.Entities;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding.Contracts;

public interface IService
{
    public Task<string> SendSearchAllLegalProceedingAsync(string login, string password,
        CancellationToken cancellationToken);
}