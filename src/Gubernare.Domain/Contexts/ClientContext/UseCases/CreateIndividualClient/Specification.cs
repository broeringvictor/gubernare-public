using Flunt.Notifications;
using Flunt.Validations;
using System.Linq;

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


        if (!string.IsNullOrWhiteSpace(request.email))
        {
            contract
                .IsEmail(request.email, "Email", "E-mail inválido")
                .IsLowerThan(request.email.Length, 150, "Email", "E-mail excede o tamanho máximo (150).");
        }


        if (!string.IsNullOrWhiteSpace(request.phone))
        {
            contract
                .IsLowerThan(request.phone.Length, 50, "Phone", "O telefone excede o tamanho máximo (50).");
        }


        if (!string.IsNullOrWhiteSpace(request.zipCode))
        {
            contract
                .IsLowerThan(request.zipCode.Length, 20, "ZipCode", "O CEP excede o tamanho máximo (20).");
        }

        if (!string.IsNullOrWhiteSpace(request.street))
        {
            contract
                .IsLowerThan(request.street.Length, 300, "Street", "A rua excede o tamanho máximo (300).");
        }

        if (!string.IsNullOrWhiteSpace(request.state))
        {
            contract
                .IsLowerThan(request.state.Length, 2, "State", "O Estado excede o tamanho máximo (2).");
        }



        if (!string.IsNullOrWhiteSpace(request.fristContact))
        {
            contract
                .IsLowerThan(request.fristContact.Length, 300, "FristContact", "O primeiro contato excede o tamanho máximo (300).");
        }

        return contract;
    }
}
