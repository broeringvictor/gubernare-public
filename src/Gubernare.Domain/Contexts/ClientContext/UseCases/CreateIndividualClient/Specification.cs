using Flunt.Notifications;
using Flunt.Validations;
using System.Linq;
using Gubernare.Domain.Contexts.SharedContext.Extensions;


namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateIndividualClient;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
    {
        var contract = new Contract<Notification>()
            .Requires()


            .IsNotNullOrEmpty(request.name, "Name", "O nome é obrigatório")
            .IsGreaterThan(request.name.Length, 2, "Name", "O nome deve conter mais que 2 caracteres")
            .IsLowerThan(request.name.Length, 300, "Name", "O nome deve conter menos que 300 caracteres");


        contract.IfNotNullOrWhiteSpace(request.email, c =>
            c.IsEmail(request.email!, "Email", "E-mail inválido")
                .IsLowerThan(request.email!.Length, 150, "Email", "E-mail excede o tamanho máximo (150)"));


        contract.IfNotNullOrWhiteSpace(request.phone,
            c => c.IsLowerThan(request.phone!.Length, 50, "Phone", "O telefone excede o tamanho máximo (10)."));

        contract.IfNotNullOrWhiteSpace(request.zipCode, c => c.NotContainsLettersOrSpecialCharacters(request.zipCode!,
                "ZipCode",
                "ZIP code must not contain letters or special characters."
            ))
            .IsLowerThan(request.zipCode!.Length, 20, "ZipCode", "O CEP excede o tamanho máximo (20).");

        contract
            .IfNotNullOrWhiteSpace(
                request.street,
                c => c.IsLowerThan(request.street!.Length, 300, "Street", "A rua excede o tamanho máximo (300).")
            )
            .IfNotNullOrWhiteSpace(
                request.state,
                c => c.IsLowerThan(request.state!.Length, 2, "State", "O Estado excede o tamanho máximo (2).")
            )
            .IfNotNullOrWhiteSpace(
                request.fristContact,
                c => c.IsLowerThan(request.fristContact!.Length, 300, "FristContact",
                    "O primeiro contato excede o tamanho máximo (300).")
            );

        return contract;
    }
}
