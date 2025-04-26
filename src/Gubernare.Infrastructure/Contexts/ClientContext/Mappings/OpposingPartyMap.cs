using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.LegalProceeding.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents;

namespace Gubernare.Infrastructure.Contexts.ClientContext.Mappings
{
    public class OpposingPartyMap : IEntityTypeConfiguration<OpposingParty>
    {
        public void Configure(EntityTypeBuilder<OpposingParty> builder)
        {
            // Nome da tabela
            builder.ToTable("OpposingParty");

            // Chave primária
            builder.HasKey(x => x.Id);

            // Exemplo de mapeamento de propriedades simples:
            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasColumnName("Phone")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(x => x.ZipCode)
                .HasColumnName("ZipCode")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.Street)
                .HasColumnName("Street")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(x => x.City)
                .HasColumnName("City")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.State)
                .HasColumnName("State")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Country)
                .HasColumnName("Country")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            // Pessoa Física
            builder.Property(x => x.JobTitle)
                .HasColumnName("JobTitle")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.MaritalStatus)
                .HasColumnName("MaritalStatus")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.Homeland)
                .HasColumnName("Homeland")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            // Pessoa Jurídica
            builder.Property(x => x.Cnpj)
                .HasColumnName("Cnpj")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.InscricaoEstadual)
                .HasColumnName("InscricaoEstadual")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(x => x.InscricaoMunicipal)
                .HasColumnName("InscricaoMunicipal")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(x => x.Lawyers)
                .HasColumnName("Lawyers")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(1000)
                .IsRequired(false);

            // PersonType (enum)
            builder.Property(x => x.PersonType)
                .HasColumnName("PersonType")
                .HasConversion<int>() // se quiser armazenar como int
                .IsRequired();
            
            builder.OwnsOne(x => x.CpfNumber, cpf =>
             {
                 cpf.Property(p => p.CpfValue)
                    .HasColumnName("CpfNumber")
                   .HasMaxLength(11);
            });
            
            builder.OwnsOne(x => x.RgNumber, rg =>
            {
                rg.Property(r => r.RgValue)
                    .HasColumnName("RgNumber")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(20)
                    .IsRequired(true);
            });
            
            builder
                .HasMany(op => op.LegalProceedings)
                .WithMany(lp => lp.OpposingParties)
                .UsingEntity<Dictionary<string, object>>(
                    "LegalProceedingOpposingParty", 
                    // Lado OpposingParty -> Tabela
                    j => j
                        .HasOne<Domain.Contexts.LegalProceeding.Entities.LegalProceeding>()
                        .WithMany()
                        .HasForeignKey("LegalProceedingId")
                        .OnDelete(DeleteBehavior.Cascade),
                    // Lado LegalProceeding -> Tabela
                    j => j
                        .HasOne<OpposingParty>()
                        .WithMany()
                        .HasForeignKey("OpposingPartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
