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

        // Configurar relação com User
        builder.HasOne(x => x.User) 
            .WithMany(x => x.CourtLogins) 
            .HasForeignKey(x => x.UserId) // Chave estrangeira para UserId
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata

        // Configurar a coluna CourtSystem
        builder.Property(x => x.CourtSystem)
            .HasColumnName("CourtSystem")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(10)
            .IsRequired(true);

        // Configurar a coluna Login
        builder.Property(x => x.Login)
            .HasColumnName("Login")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30)
            .IsRequired(true);

        // Configurar a propriedade Password como um objeto agregado
        builder.OwnsOne(x => x.Password)
            .Property(x => x.Cipher) // Acesso à propriedade Hash
            .HasColumnName("PasswordHash")
            .IsRequired();
    }
}