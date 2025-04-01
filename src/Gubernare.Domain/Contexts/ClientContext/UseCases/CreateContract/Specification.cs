using Flunt.Notifications;
using Flunt.Validations;

namespace Gubernare.Domain.Contexts.ClientContext.UseCases.CreateContract;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
    {
        var contract = new Contract<Notification>()
            .Requires()

            // Validações para Name (campo obrigatório)
            .IsNotNullOrEmpty(request.Name, "Name", "O nome é obrigatório")
            .IsGreaterThan(request.Name?.Length ?? 0, 2, "Name", "O nome deve conter mais que 2 caracteres")
            .IsLowerThan(request.Name?.Length ?? 0, 300, "Name", "O nome deve conter menos que 300 caracteres")

            // Validações para Description (campo obrigatório)
            .IsNotNullOrEmpty(request.Description, "Description", "A descrição é obrigatória")
            .IsLowerThan(request.Description?.Length ?? 0, 300, "Description", "A descrição deve conter menos que 300 caracteres");

        // Validações para Type (campo opcional)
        // Se vier preenchido, valida tamanho; se vier null ou vazio, não valida.
        if (!string.IsNullOrWhiteSpace(request.Type))
        {
            contract
                .IsLowerThan(request.Type.Length, 100, "Type", "O tipo deve conter menos que 100 caracteres");
        }

        // Validações para Notes (campo opcional)
        if (!string.IsNullOrWhiteSpace(request.Notes))
        {
            contract
                .IsLowerThan(request.Notes.Length, 600, "Notes", "As notas devem conter menos que 600 caracteres");
        }

        // Validações para Price (campo opcional)
        // Se vier preenchido, valida se é maior que zero.
        if (request.Price.HasValue)
        {
            contract
                .IsGreaterThan(request.Price.Value, 0, "Price", "O preço deve ser maior que zero");
        }

        // Validações para DocumentFolder (campo opcional)
        if (!string.IsNullOrWhiteSpace(request.DocumentFolder))
        {
            contract
                .IsLowerThan(request.DocumentFolder.Length, 10, "Document Folder", "A pasta de documentos deve conter menos que 10 caracteres");
        }

        return contract;
    }
}
