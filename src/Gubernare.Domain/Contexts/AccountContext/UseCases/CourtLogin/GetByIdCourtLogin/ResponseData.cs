namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.GetByIdCourtLogin;

public record ResponseData(string CourtSystem, Guid Id, string Login, string Password);