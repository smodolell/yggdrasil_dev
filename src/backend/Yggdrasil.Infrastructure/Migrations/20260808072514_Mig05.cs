using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yggdrasil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FI_Persona_FI_Perfil_FI_PerfilId",
                table: "FI_Persona");

            migrationBuilder.DropIndex(
                name: "IX_FI_Persona_FI_PerfilId",
                table: "FI_Persona");

            migrationBuilder.DropColumn(
                name: "FI_PerfilId",
                table: "FI_Persona");

            migrationBuilder.AddForeignKey(
                name: "FK_FI_Persona_FI_Perfil_PerfilId",
                table: "FI_Persona",
                column: "PerfilId",
                principalTable: "FI_Perfil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FI_Persona_FI_Perfil_PerfilId",
                table: "FI_Persona");

            migrationBuilder.AddColumn<int>(
                name: "FI_PerfilId",
                table: "FI_Persona",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FI_Persona_FI_PerfilId",
                table: "FI_Persona",
                column: "FI_PerfilId");

            migrationBuilder.AddForeignKey(
                name: "FK_FI_Persona_FI_Perfil_FI_PerfilId",
                table: "FI_Persona",
                column: "FI_PerfilId",
                principalTable: "FI_Perfil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
