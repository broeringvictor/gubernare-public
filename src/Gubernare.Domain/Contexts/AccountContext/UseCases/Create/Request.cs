using System.ComponentModel;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.Create;

public record Request(

    [SwaggerSchema("Nome completo do usuário")]
    string Name,
    [SwaggerSchema("Melhor e-mail para contato")]
    string Email,
    [SwaggerSchema("Senha (mínimo 8 caracteres)")]
    string Password
) : IRequest<Response>;
