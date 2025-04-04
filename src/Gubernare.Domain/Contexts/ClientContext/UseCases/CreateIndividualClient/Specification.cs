using Flunt.Notifications;
using Flunt.Validations;
using Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient;
using Gubernare.Domain.Contexts.SharedContext.Extensions;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
    {
        var contract = new Contract<Notification>();

        // Verifica se o request é nulo antes de validar suas propriedades
        if (request == null)
        {
            contract.AddNotification("Request", "A requisição não pode ser nula.");
            return contract;
        }

        contract
            .IsNotNullOrEmpty(request.name, "Name", "O nome é obrigatório")
            .IsGreaterThan(request.name.Length, 2, "Name", "O nome deve conter mais que 2 caracteres")
            .IsLowerThan(request.name.Length, 300, "Name", "O nome deve conter menos que 300 caracteres");

        // Validação condicional do email (só executa se email não for nulo/vazio)
        contract.IfNotNullOrWhiteSpace(
            request.email,
            c => c.IsEmail(request.email, "Email", "E-mail inválido")
                  .IsLowerThan(request.email.Length, 150, "Email", "E-mail excede o tamanho máximo (150)")
        );

        // Validação condicional do telefone
        contract.IfNotNullOrWhiteSpace(
            request.phone,
            c => c.IsLowerThan(request.phone.Length, 50, "Phone", "O telefone excede o tamanho máximo (10).")
        );

        // Validação condicional do CEP
        contract.IfNotNullOrWhiteSpace(
            request.zipCode,
            c => c.NotContainsLettersOrSpecialCharacters(request.zipCode, "ZipCode", "CEP não pode conter letras ou caracteres especiais.")
                  .IsLowerThan(request.zipCode.Length, 20, "ZipCode", "O CEP excede o tamanho máximo (20).")
        );

        // Validação condicional da rua
        contract.IfNotNullOrWhiteSpace(
            request.street,
            c => c.IsLowerThan(request.street.Length, 300, "Street", "A rua excede o tamanho máximo (300).")
        );


        contract.IfNotNullOrWhiteSpace(
            request.state,
            c => c.IsLowerThan(request.state.Length, 2, "State", "O Estado excede o tamanho máximo (2).")
        );


        contract.IfNotNullOrWhiteSpace(
            request.fristContact, // ✅ Nome correto
            c => c.IsLowerThan(request.fristContact!.Length, 300, "FristContact", "O primeiro contato excede o tamanho máximo (300).")
        );

        return contract;
    }
}
