using Gubernare.Domain.Contexts.LegalProceeding.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gubernare.Infrastructure.Contexts.LegalProeeding.Mappings
{
    public class LegalProceedingEventMap : IEntityTypeConfiguration<LegalProceedingEvent>
    {
        public void Configure(EntityTypeBuilder<LegalProceedingEvent> builder)
        {
            // Nome da tabela no banco
            builder.ToTable("LegalProceedingEvent");

            // Chave primária
            builder.HasKey(x => x.Id);

            // Mapeia a FK no campo .LegalProceedingId
            builder.Property(x => x.LegalProceedingId)
                .HasColumnName("LegalProceedingId")
                .IsRequired();

            // Propriedades simples
            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(x => x.Date)
                .HasColumnName("Date")
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName("Type")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LegalDeadline)
                .HasColumnName("LegalDeadline")
                .IsRequired(false);


        }
    }
}
