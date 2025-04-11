using MediatR;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.GetByIdCourtLogin;

public record Request(
    Guid UserId,
    Guid Id
) : IRequest<Response>;
