using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamenJuanma.Migrations
{
    /// <inheritdoc />
    public partial class mssqlonprem_migration_187 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Reservas");

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraInicio",
                table: "Reservas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraSalida",
                table: "Reservas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "HoraSalida",
                table: "Reservas");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
