using MediatR;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.Authenticate;

public record Request(
    string Email,
    string Password
) : IRequest<Response>;