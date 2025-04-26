using Flunt.Notifications;
using Flunt.Validations;

namespace Gubernare.Domain.Contexts.LegalProceeding.UseCases.ToDo.Create;

public static class Specification
{
        public static Contract<Notification> Ensure(Request request)
        {
            var contract = new Contract<Notification>()
                .Requires()
                // Title (obrigatório, 2-200 caracteres)
                .IsNotNullOrWhiteSpace(request.Title,  "Title", "O título é obrigatório")
                .IsGreaterThan       (request.Title?.Length ?? 0,  2, "Title", "O título deve conter mais que 2 caracteres")
                .IsLowerThan         (request.Title?.Length ?? 0, 200, "Title", "O título deve conter menos que 200 caracteres")
                // Description (opcional, máx 600)
                .IsLowerThan         (request.Description?.Length ?? 0, 600, "Description", "A descrição deve conter menos que 600 caracteres");

            if (request.DueDate.HasValue)
                contract
                    .IsGreaterThan(request.DueDate.Value, DateTime.Now, "DueDate", "A data de vencimento deve ser futura");

            return contract;
        }
}

