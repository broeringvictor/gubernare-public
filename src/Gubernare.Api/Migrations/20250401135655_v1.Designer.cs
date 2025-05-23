﻿// <auto-generated />
using System;
using Gubernare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gubernare.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250401135655_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContractIndividualClient", b =>
                {
                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IndividualClientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContractId", "IndividualClientId");

                    b.HasIndex("IndividualClientId");

                    b.ToTable("ContractIndividualClient");
                });

            modelBuilder.Entity("Gubernare.Domain.Contexts.AccountContext.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Gubernare.Domain.Contexts.AccountContext.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Gubernare.Domain.Contexts.ClientContext.Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Description");

                    b.Property<string>("DocumentFolder")
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("DocumentFolder");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("Notes")
                        .HasMaxLength(600)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Notes");

                    b.Property<decimal?>("Price")
                        .HasColumnType("DECIMAL(18,2)")
                        .HasColumnName("Price");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("StartDate");

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.ToTable("Contract", (string)null);
                });

            modelBuilder.Entity("Gubernare.Domain.Contexts.ClientContext.Entities.IndividualClient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("BirthDate");

                    b.Property<string>("City")
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("City");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Country");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("FristContact")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("FristContact");

                    b.Property<DateTime>("FristContactAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("FristContactAt");

                    b.Property<string>("Homeland")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Homeland");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("JobTitle");

                    b.Property<string>("MaritalStatus")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("MaritalStatus");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("Notes")
                        .HasMaxLength(600)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Notes");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Phone");

                    b.Property<string>("State")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("State");

                    b.Property<string>("Street")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Street");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("IndividualClient", (string)null);
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("ContractIndividualClient", b =>
                {
                    b.HasOne("Gubernare.Domain.Contexts.ClientContext.Entities.Contract", null)
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gubernare.Domain.Contexts.ClientContext.Entities.IndividualClient", null)
                        .WithMany()
                        .HasForeignKey("IndividualClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gubernare.Domain.Contexts.AccountContext.Entities.User", b =>
                {
                    b.OwnsOne("Gubernare.Domain.Contexts.AccountContext.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.OwnsOne("Gubernare.Domain.Contexts.AccountContext.ValueObjects.Verification", "Verification", b2 =>
                                {
                                    b2.Property<Guid>("EmailUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Code")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("EmailVerificationCode");

                                    b2.Property<DateTime?>("ExpiresAt")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("EmailVerificationExpiresAt");

                                    b2.Property<DateTime?>("VerifiedAt")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("EmailVerificationVerifiedAt");

                                    b2.HasKey("EmailUserId");

                                    b2.ToTable("User");

                                    b2.WithOwner()
                                        .HasForeignKey("EmailUserId");
                                });

                            b1.Navigation("Verification")
                                .IsRequired();
                        });

                    b.OwnsOne("Gubernare.Domain.Contexts.AccountContext.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PasswordHash");

                            b1.Property<string>("ResetCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PasswordResetCode");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("Gubernare.Domain.Contexts.ClientContext.Entities.IndividualClient", b =>
                {
                    b.OwnsOne("Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents.Cpf", "CpfNumber", b1 =>
                        {
                            b1.Property<Guid>("IndividualClientId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CpfValue")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("VARCHAR")
                                .HasColumnName("CpfNumber");

                            b1.HasKey("IndividualClientId");

                            b1.ToTable("IndividualClient");

                            b1.WithOwner()
                                .HasForeignKey("IndividualClientId");
                        });

                    b.OwnsOne("Gubernare.Domain.Contexts.SharedContext.ValueObjects.Documents.Rg", "RgNumber", b1 =>
                        {
                            b1.Property<Guid>("IndividualClientId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("RgValue")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("VARCHAR")
                                .HasColumnName("RgNumber");

                            b1.HasKey("IndividualClientId");

                            b1.ToTable("IndividualClient");

                            b1.WithOwner()
                                .HasForeignKey("IndividualClientId");
                        });

                    b.Navigation("CpfNumber")
                        .IsRequired();

                    b.Navigation("RgNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("Gubernare.Domain.Contexts.AccountContext.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gubernare.Domain.Contexts.AccountContext.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
