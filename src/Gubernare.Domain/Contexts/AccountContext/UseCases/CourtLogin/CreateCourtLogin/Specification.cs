using Flunt.Notifications;
using Flunt.Validations;

namespace Gubernare.Domain.Contexts.AccountContext.UseCases.CourtLogin.CreateCourtLogin;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
    {
        var contract = new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.Login.Length, 3, "Login", "O login deve conter mais de 3 caracteres.")
            .IsLowerThan(request.Login.Length, 10, "Login", "O login deve conter menos de 10 caracteres.")
            .IsGreaterThan(request.CourtSystem.Length, 3, "CourtSystem",
                "O nome do tribunal deve conter mais de 3 caracteres.")
            .IsLowerThan(request.CourtSystem.Length, 10, "CourtSystem",
                "O nome do tribunal deve conter menos de 10 caracteres.")
            .IsGreaterThan(request.Password.Length, 6, "Password", "A senha deve conter mais de 6 caracteres.")
            .IsLowerThan(request.Password.Length, 20, "Password", "A senha deve conter menos de 20 caracteres.");

        return contract;
    }
}