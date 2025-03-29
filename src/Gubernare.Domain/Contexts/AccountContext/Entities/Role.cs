using Gubernare.Domain.Contexts.SharedContext.Entities;

namespace Gubernare.Domain.Contexts.AccountContext.Entities;

public class Role : Entity
{
    public string Name { get; set; } = string.Empty;

    public List<User> Users { get; set; } = new();
}