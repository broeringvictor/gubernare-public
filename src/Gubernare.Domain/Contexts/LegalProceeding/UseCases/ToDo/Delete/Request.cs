using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Delete;

public sealed record Request(Guid Id) : IRequest<Response>;