using Flunt.Notifications;
using Flunt.Validations;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()

            .IsNotNullOrEmpty(request.Name, "Name", "O nome é obrigatório")
            .IsGreaterThan(request.Name.Length, 2, "Name", "O nome deve conter mais que 2 caracteres")
            .IsLowerThan(request.Name.Length, 300, "Name", "O nome deve conter menos que 300 caracteres")
            .IsGreaterThan(request.Name.Length, 2, "Name", "O nome deve conter mais que 2 caracteres")

            .IsLowerThan(request.Type!.Length, 100, "Type", "O tipo deve conter menos que 100 caracteres")

            .IsNotNullOrEmpty(request.Description, "Description", "A descrição é obrigatória")
            .IsLowerThan(request.Description.Length, 300, "Description",
                "A descrição deve conter menos que 300 caracteres")


            .IsLowerThan(request.Notes!.Length, 600, "Notes", "As notas devem conter menos que 600 caracteres")


            .IsGreaterThan(request.Price!.Value, 0, "Price", "O preço deve ser maior que zero")


            .IsLowerThan(request.DocumentFolder!.Length, 10, "Document Folder",
                "A pasta de documentos deve conter menos que 10 caracteres");
}
