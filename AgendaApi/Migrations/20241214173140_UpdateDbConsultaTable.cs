using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbConsultaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Pacientes_PacienteId",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Profissionais_ProfissionalId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_ProfissionalId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "ProfissionalId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Consultas");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraConsulta",
                table: "Consultas",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpfPaciente",
                table: "Consultas",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomePaciente",
                table: "Consultas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeProfissional",
                table: "Consultas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegistroProfissional",
                table: "Consultas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpfPaciente",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "NomePaciente",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "NomeProfissional",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "RegistroProfissional",
                table: "Consultas");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraConsulta",
                table: "Consultas",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Consultas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId",
                table: "Consultas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Consultas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_ProfissionalId",
                table: "Consultas",
                column: "ProfissionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Pacientes_PacienteId",
                table: "Consultas",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "PacienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Profissionais_ProfissionalId",
                table: "Consultas",
                column: "ProfissionalId",
                principalTable: "Profissionais",
                principalColumn: "ProfissionalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
