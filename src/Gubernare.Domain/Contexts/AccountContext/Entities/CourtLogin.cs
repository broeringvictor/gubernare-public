using Gubernare.Domain.Contexts.AccountContext.ValueObjects;
using Gubernare.Domain.Contexts.SharedContext.Entities;

namespace Gubernare.Domain.Contexts.AccountContext.Entities;

public class CourtLogin : Entity
{
    protected CourtLogin()
    {
    }

    public CourtLogin(Guid userId, string courtSystem, string login, Password password)
    {
        UserId = userId;
        CourtSystem = courtSystem;
        Login = login;
        Password = password;
    }

    // Chave estrangeira para o usuário
    public Guid UserId { get; private set; }

    // Propriedade de navegação para o User
    public User User { get; private set; }

    public string CourtSystem { get; private set; }
    public string Login { get; private set; }
    public Password Password { get; private set; }
}