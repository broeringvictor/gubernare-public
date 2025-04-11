using Gubernare.Domain.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gubernare.Infrastructure.Contexts.AccountContext.Mappings;

public class CourtLoginMap : IEntityTypeConfiguration<CourtLogin>
{
    public void Configure(EntityTypeBuilder<CourtLogin> builder)
    {
        builder.ToTable("CourtLogin");
        builder.HasKey(x => x.Id);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired(true);
        
        builder.Property(x => x.CourtSystem)
            .HasColumnName("CourtSystem")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(10)
            .IsRequired(true);
        
        builder.Property(x => x.Login)
            .HasColumnName("Login")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30)
            .IsRequired(true);
        
        builder.OwnsOne(x => x.Password)
            .Property(x => x.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();
    }
}