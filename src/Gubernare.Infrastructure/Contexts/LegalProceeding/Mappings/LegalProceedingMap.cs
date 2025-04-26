using Gubernare.Domain.Contexts.ClientContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.Mappings
{
    public class LegalProceedingMap : IEntityTypeConfiguration<Domain.Contexts.LegalProceeding.Entities.LegalProceeding>
    {
        public void Configure(EntityTypeBuilder<Domain.Contexts.LegalProceeding.Entities.LegalProceeding> builder)
        {
            // Nome da Tabela
            builder.ToTable("LegalProceeding");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Campos (Propriedades simples)
            builder.Property(x => x.Number)
                .HasColumnName("Number")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.ClientRole)
                .HasColumnName("ClientRole")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.OpposingPartyRole)
                .HasColumnName("OpposingPartyRole")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired(false);

            // Se quiser salvar CourtInstance como int no banco (ou string), use .HasConversion
            builder.Property(x => x.CourtInstance)
                .HasColumnName("CourtInstance")
                .HasConversion<int?>()  // armazena enum como int? no banco
                .IsRequired(false);

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(x => x.LegalCourt)
                .HasColumnName("LegalCourt")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);
            
            builder.Property(x => x.CourtDivisionName)
                .HasColumnName("CourtDivisionName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);
            
            builder.Property(x => x.CauseValue)
                .HasColumnName("CauseValue")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.AccessCode)
                .HasColumnName("AccessCode")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.DistributionDate)
                .HasColumnName("Date")
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName("Type")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.FinishedDateTime)
                .HasColumnName("FinishedDateTime")
                .IsRequired(false);


            builder
                .HasMany(lp => lp.LegalProceedingEvents)
                .WithOne(lpe => lpe.LegalProceeding)   // Propriedade de navegação
                .HasForeignKey(lpe => lpe.LegalProceedingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(lp => lp.OpposingParties)
                .WithMany(op => op.LegalProceedings)
                .UsingEntity<Dictionary<string, object>>(
                    // Nome da tabela de junção
                    "LegalProceedingOpposingParty",
                    // Configuração do lado OpposingParty
                    x => x
                        .HasOne<OpposingParty>()
                        .WithMany()
                        .HasForeignKey("OpposingPartyId")
                        .OnDelete(DeleteBehavior.Cascade),
                    // Configuração do lado LegalProceeding
                    x => x
                        .HasOne<Domain.Contexts.LegalProceeding.Entities.LegalProceeding>()
                        .WithMany()
                        .HasForeignKey("LegalProceedingId")
                        .OnDelete(DeleteBehavior.Cascade)
                );
            
            builder
                .HasMany(lp => lp.IndividualClients)
                .WithMany(op => op.LegalProceedings)
                .UsingEntity<Dictionary<string, object>>(
                    // Nome da tabela de junção
                    "ListIndividualClientLegalProceeding",
                    // Configuração do lado OpposingParty
                    x => x
                        .HasOne<IndividualClient>()
                        .WithMany()
                        .HasForeignKey("IndividualClientsId")
                        .OnDelete(DeleteBehavior.Cascade),
                    // Configuração do lado LegalProceeding
                    x => x
                        .HasOne<Domain.Contexts.LegalProceeding.Entities.LegalProceeding>()
                        .WithMany()
                        .HasForeignKey("LegalProceedingId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

        }
    }
}
