using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding;

public record Request(
    Guid Id
) : IRequest<Response>;
