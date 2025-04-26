using Gubernare.Domain.Contexts.LegalProceeding.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// <- ajuste se o namespace for outro

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.Mappings;

public sealed class ToDoMap : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        // ---------- Tabela ----------
        builder.ToTable("ToDo");

        // ---------- PK ----------
        builder.HasKey(t => t.Id);

        // ---------- Colunas ----------
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(300)
            .HasColumnType("nvarchar(300)");

        builder.Property(t => t.Description)
            .HasMaxLength(2000)
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(t => t.DueDate)
            .HasColumnType("datetime2");

        builder.Property(t => t.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        // ---------- Índices ----------
        builder.HasIndex(t => t.IsCompleted);
        builder.HasIndex(t => new { t.UserId, t.ProcessId, t.IsCompleted });

        // ---------- Relacionamentos ----------
        builder.HasOne(t => t.Process)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProcessId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}