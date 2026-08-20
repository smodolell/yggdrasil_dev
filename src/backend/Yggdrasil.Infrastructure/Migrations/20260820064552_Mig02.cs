using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yggdrasil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CS_TablaAmortiza_CS_Credito_CS_CreditoId",
                table: "CS_TablaAmortiza");

            migrationBuilder.DropForeignKey(
                name: "FK_CS_TablaAmortiza_FI_Credito_CreditoId",
                table: "CS_TablaAmortiza");

            migrationBuilder.DropIndex(
                name: "IX_CS_TablaAmortiza_CS_CreditoId",
                table: "CS_TablaAmortiza");

            migrationBuilder.DropColumn(
                name: "CS_CreditoId",
                table: "CS_TablaAmortiza");

            migrationBuilder.AddForeignKey(
                name: "FK_CS_TablaAmortiza_CS_Credito_CreditoId",
                table: "CS_TablaAmortiza",
                column: "CreditoId",
                principalTable: "CS_Credito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CS_TablaAmortiza_CS_Credito_CreditoId",
                table: "CS_TablaAmortiza");

            migrationBuilder.AddColumn<int>(
                name: "CS_CreditoId",
                table: "CS_TablaAmortiza",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CS_TablaAmortiza_CS_CreditoId",
                table: "CS_TablaAmortiza",
                column: "CS_CreditoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CS_TablaAmortiza_CS_Credito_CS_CreditoId",
                table: "CS_TablaAmortiza",
                column: "CS_CreditoId",
                principalTable: "CS_Credito",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CS_TablaAmortiza_FI_Credito_CreditoId",
                table: "CS_TablaAmortiza",
                column: "CreditoId",
                principalTable: "FI_Credito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
