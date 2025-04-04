using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gubernare.Api.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RgNumber",
                table: "IndividualClient",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CpfNumber",
                table: "IndividualClient",
                type: "VARCHAR(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)",
                oldMaxLength: 11);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RgNumber",
                table: "IndividualClient",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CpfNumber",
                table: "IndividualClient",
                type: "VARCHAR(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)",
                oldMaxLength: 11,
                oldNullable: true);
        }
    }
}
