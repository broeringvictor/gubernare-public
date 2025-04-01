using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Infrastructure.Contexts.AccountContext.Mappings;
using Gubernare.Infrastructure.Contexts.ClientContext.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Gubernare.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<IndividualClient> IndividualClients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new ContractMap());
        modelBuilder.ApplyConfiguration(new IndividualClientMap());

    }
}
