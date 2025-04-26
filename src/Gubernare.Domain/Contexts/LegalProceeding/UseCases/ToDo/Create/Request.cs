using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Create;

public sealed record Request(
    string      Title,
    string?     Description,
    DateTime?   DueDate,
    Guid?        ProcessId,
    Guid        UserId
) : IRequest<Response>;