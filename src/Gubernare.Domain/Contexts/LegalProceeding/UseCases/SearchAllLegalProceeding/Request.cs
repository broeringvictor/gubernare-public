using System.Text.Json.Serialization;
using MediatR;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding;

public record Request : IRequest<Response>
{
    [JsonIgnore]
    public Guid Id { get; init; }

    
}