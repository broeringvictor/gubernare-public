using Gubernare.Domain.Contexts.AccountContext.Entities;
using MediatR;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CreateCourtLogin;

public record Request(
    User UserId,
    string CourtSystem,
    string Login,
    string Password

) : IRequest<Response>;
