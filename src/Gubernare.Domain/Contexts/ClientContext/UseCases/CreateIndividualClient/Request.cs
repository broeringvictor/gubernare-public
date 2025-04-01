using Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;
using MediatR;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient;

// Manter o construtor do record com o parâmetro "name",
// mas criar a propriedade pública "Name" que retorna sempre em minúsculo.
public record Request(
    string name,
    string? email,
    string? phone,
    string? zipCode,
    string? street,
    string? city,
    string? state,
    string? country,
    string? jobTitle,
    string? maritalStatus,
    string? homeland,
    string? cpfNumber,
    string? rgNumber,
    DateTime? birthDate,
    string? fristContact
) : IRequest<Response>;

