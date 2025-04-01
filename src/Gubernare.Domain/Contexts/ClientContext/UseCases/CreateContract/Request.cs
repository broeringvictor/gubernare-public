using MediatR;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract;

public record Request(
    string Name,
    string? Type,
    string Description,
    string? Notes,
    DateTime? StartDate,
     DateTime? EndDate,
    decimal? Price,
    string? DocumentFolder

) : IRequest<Response>;
