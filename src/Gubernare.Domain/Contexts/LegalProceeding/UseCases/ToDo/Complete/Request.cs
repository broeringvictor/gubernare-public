using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Complete;

public sealed record Request(Guid Id) : IRequest<Response>;