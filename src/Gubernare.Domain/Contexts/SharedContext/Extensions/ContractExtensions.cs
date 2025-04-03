using Flunt.Notifications;
using Flunt.Validations;

namespace Gubernare.Domain.Contexts.SharedContext.Extensions;

public static class ContractExtensions
{
    public static Contract<Notification> IfNotNullOrWhiteSpace(this Contract<Notification> contract, string? value,
        Action<Contract<Notification>> rule)
    {
        if (!string.IsNullOrWhiteSpace(value))
            rule(contract);
        return contract;
    }

    public static Contract<Notification> NotContainsLettersOrSpecialCharacters(this Contract<Notification> contract,
        string? value, string propertyName, string message = "The field cannot contain letters or special characters.")
    {
        if (!string.IsNullOrWhiteSpace(value) &&
            System.Text.RegularExpressions.Regex.IsMatch(value, @"[^\d\s]")) // Permite apenas números e espaços
        {
            contract.AddNotification(propertyName, message);
        }

        return contract;
    }
}
