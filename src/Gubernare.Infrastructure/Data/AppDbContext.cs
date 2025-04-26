using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.LegalProceeding.Entities;
using Gubernare.Infrastructure.Contexts.AccountContext.Mappings;
using Gubernare.Infrastructure.Contexts.ClientContext.Mappings;
using Gubernare.Infrastructure.Contexts.LegalProceeding.Mappings;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

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
    public DbSet<CourtLogin> CourtLogins { get; set; } = null!;
    public DbSet<OpposingParty> OpposingParties { get; set; } = null!;
    public DbSet<LegalProceeding> LegalProceedings { get; set; } = null!;
    public DbSet<LegalProceedingEvent> LegalProceedingEvents { get; set; } = null!;
    public DbSet<ToDo> ToDos { get; set; } = null!;
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new ContractMap());
        modelBuilder.ApplyConfiguration(new IndividualClientMap());
        modelBuilder.ApplyConfiguration(new CourtLoginMap());
        modelBuilder.ApplyConfiguration(new OpposingPartyMap());
        modelBuilder.ApplyConfiguration(new LegalProceedingEventMap());
        modelBuilder.ApplyConfiguration(new LegalProceedingMap());
        modelBuilder.ApplyConfiguration(new ToDoMap());

    }
}
