using Flunt.Notifications;
using Flunt.Validations;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Update;

internal static class Specification
{
    public static Contract<Notification> Ensure(Request request)
    {
        var contract = new Contract<Notification>();
            
            

        // Se Title vier preenchido, valida tamanho
        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            contract
                .IsGreaterThan(request.Title.Length, 2,   "Title", "O título deve conter mais que 2 caracteres")
                .IsLowerThan  (request.Title.Length, 200, "Title", "O título deve conter menos que 200 caracteres");
        }

        // Se Description vier preenchida, valida tamanho
        if (!string.IsNullOrWhiteSpace(request.Description))
            contract.IsLowerThan(request.Description.Length, 600,
                "Description", "A descrição deve conter menos que 600 caracteres");


        // Pelo menos um campo (além do Id) deve ser informado
        if (request.Title is null &&
            request.Description is null &&
            request.DueDate is null &&
            request.IsCompleted is null)
        {
            contract.AddNotification("Request", "Nenhum campo foi informado para alteração");
        }

        return contract;
    }
}