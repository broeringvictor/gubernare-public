using MediatR;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.GetAllCourtLogin;

public record Request(
    Guid UserId
) : IRequest<Response>;
