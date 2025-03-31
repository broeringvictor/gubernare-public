using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gubernare.Infrastructure.Contexts.ClientContext.Mappings;

public class ContractMap : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contract");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300)
            .IsRequired(true);

        builder.Property(x => x.Type)
            .HasColumnName("Type")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Type)
            .HasColumnName("Type")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300)
            .IsRequired(false);

        builder.Property(x => x.Notes)
            .HasColumnName("Notes")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(600)
            .IsRequired(false);

        builder.Property(x => x.StartDate)
            .HasColumnName("StartDate")
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.Property(x => x.EndDate)
            .HasColumnName("EndDate")
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.Property(x => x.Price)
            .HasColumnName("Price")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired(false);

        builder.Property(x => x.DocumentFolder)
            .HasColumnName("DocumentFolder")
            .HasColumnType("VARCHAR")
            .HasMaxLength(10)
            .IsRequired(false);

        builder
            .HasMany(x => x.IndividualClient)
            .WithMany(x => x.Contracts)
            .UsingEntity<Dictionary<string, object>>(
                "ContractIndividualClient",
                individualClient => individualClient
                    .HasOne<IndividualClient>()
                    .WithMany()
                    .HasForeignKey("IndividualClientId")
                    .OnDelete(DeleteBehavior.Cascade),
                contract => contract
                    .HasOne<Contract>()
                    .WithMany()
                    .HasForeignKey("ContractId")
                    .OnDelete(DeleteBehavior.Cascade));
    }






}