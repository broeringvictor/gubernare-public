using Gubernare.Domain.Contexts.ClientContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gubernare.Infrastructure.Contexts.ClientContext.Mappings
{
    public class IndividualClientMap : IEntityTypeConfiguration<IndividualClient>
    {
        public void Configure(EntityTypeBuilder<IndividualClient> builder)
        {
            builder.ToTable("IndividualClient");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(300)
                .IsRequired(true);

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(600)
                .IsRequired(false);

            builder.Property(x => x.Phone)
                .HasColumnName("Phone")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(x => x.ZipCode)
                .HasColumnName("ZipCode")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.Street)
                .HasColumnName("Street")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(x => x.City)
                .HasColumnName("City")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(x => x.State)
                .HasColumnName("State")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Country)
                .HasColumnName("Country")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.JobTitle)
                .HasColumnName("JobTitle")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.MaritalStatus)
                .HasColumnName("MaritalStatus")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Homeland)
                .HasColumnName("Homeland")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.BirthDate)
                .HasColumnName("BirthDate")
                .HasColumnType("DATETIME")
                .IsRequired(false);

            builder.Property(x => x.FristContactAt)
                .HasColumnName("FristContactAt")
                .HasColumnType("DATETIME")
                .IsRequired(true);

            builder.Property(x => x.FristContact)
                .HasColumnName("FristContact")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(300)
                .IsRequired(false);

            // Mapeamento de tipos de valor para CpfNumber e RgNumber.
            // Considera-se que estes tipos possuam uma propriedade 'Number' que armazena o valor.
            builder.OwnsOne(x => x.CpfNumber, cpf =>
            {
                cpf.Property(c => c.CpfValue)
                    .HasColumnName("CpfNumber")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(11)
                    .IsRequired(true);
            });

            builder.OwnsOne(x => x.RgNumber, rg =>
            {
                rg.Property(r => r.RgValue)
                    .HasColumnName("RgNumber")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(20)
                    .IsRequired(true);
            });


        }
    }
}
