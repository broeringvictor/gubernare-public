using MediatR;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin;

public record Request(
    Guid UserId,
    string CourtSystem,
    string Login,
    string Password

) : IRequest<Response>;
