using Gubernare.Domain.Contexts.AccountContext.ValueObjects;
using Gubernare.Domain.Contexts.SharedContext.Entities;

namespace Gubernare.Domain.Contexts.AccountContext.Entities;

public class CourtLogin(
    User userId,
    string courtSystem,
    string login,
    Password password)
    : Entity
{
    public User UserId { get; private set; } = userId;
    public string CourtSystem{ get; private set; } = courtSystem;
    public string Login { get; private set; } = login;
    public Password Password { get; private set; } = password;
}