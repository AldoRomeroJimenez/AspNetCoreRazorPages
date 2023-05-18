using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreIdentity.Migrations
{
    public partial class ActualizacionEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FechaNacimiento",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CertificadoSAT",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeySAT",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificadoSAT",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "KeySAT",
                table: "Empresa");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Empresa",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
