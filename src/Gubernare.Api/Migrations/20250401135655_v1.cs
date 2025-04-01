using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gubernare.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    Notes = table.Column<string>(type: "NVARCHAR(600)", maxLength: 600, nullable: true),
                    StartDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    EndDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Price = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    DocumentFolder = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualClient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    Notes = table.Column<string>(type: "NVARCHAR(600)", maxLength: 600, nullable: true),
                    Phone = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    ZipCode = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: true),
                    Street = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    City = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    State = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    JobTitle = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    MaritalStatus = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Homeland = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    CpfNumber = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    RgNumber = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    FristContactAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FristContact = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractIndividualClient",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndividualClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractIndividualClient", x => new { x.ContractId, x.IndividualClientId });
                    table.ForeignKey(
                        name: "FK_ContractIndividualClient_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractIndividualClient_IndividualClient_IndividualClientId",
                        column: x => x.IndividualClientId,
                        principalTable: "IndividualClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractIndividualClient_IndividualClientId",
                table: "ContractIndividualClient",
                column: "IndividualClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractIndividualClient");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "IndividualClient");
        }
    }
}
