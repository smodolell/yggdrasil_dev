using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yggdrasil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Empresa",
                table: "CAT_Empresa");

            migrationBuilder.RenameTable(
                name: "CAT_Empresa",
                newName: "CAT_Empresas");

            migrationBuilder.AlterColumn<string>(
                name: "NomEmpresa",
                table: "CAT_Empresas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Empresas",
                table: "CAT_Empresas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CAT_Monedas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomMoneda = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ClaveMoneda = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    PorDefecto = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Monedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Periodicidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ClavePeriodicidad = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    NomPeriodicidad = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    ParamDias = table.Column<short>(type: "smallint", nullable: false),
                    ParamMes = table.Column<short>(type: "smallint", nullable: false),
                    NroPagosAnio = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    NroPagosMes = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    UsaDias = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Periodicidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Plazos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorPlazo = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Plazos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Tasa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorTasa = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    NomTasa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EsVariable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Tasa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TasasIva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorTasa = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    NomTasaIva = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TasasIva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TasaValor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TasaId = table.Column<int>(type: "int", nullable: false),
                    ValorTasa = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TasaValor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_TasaValor_CAT_Tasa_TasaId",
                        column: x => x.TasaId,
                        principalTable: "CAT_Tasa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Empresas_NomEmpresa",
                table: "CAT_Empresas",
                column: "NomEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Monedas_ClaveMoneda",
                table: "CAT_Monedas",
                column: "ClaveMoneda",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Periodicidades_ClavePeriodicidad",
                table: "CAT_Periodicidades",
                column: "ClavePeriodicidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Plazos_ValorPlazo",
                table: "CAT_Plazos",
                column: "ValorPlazo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tasa_Activo",
                table: "CAT_Tasa",
                column: "Activo");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tasa_Activo_EsVariable",
                table: "CAT_Tasa",
                columns: new[] { "Activo", "EsVariable" });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tasa_EsVariable",
                table: "CAT_Tasa",
                column: "EsVariable");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tasa_NomTasa_Unique",
                table: "CAT_Tasa",
                column: "NomTasa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TasasIva_NomTasaIva",
                table: "CAT_TasasIva",
                column: "NomTasaIva");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TasaValor_Fecha",
                table: "CAT_TasaValor",
                column: "Fecha");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TasaValor_TasaId",
                table: "CAT_TasaValor",
                column: "TasaId");

            migrationBuilder.CreateIndex(
                name: "UK_CAT_TasaValor_TasaId_Fecha",
                table: "CAT_TasaValor",
                columns: new[] { "TasaId", "Fecha" },
                unique: true,
                filter: "[Fecha] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Monedas");

            migrationBuilder.DropTable(
                name: "CAT_Periodicidades");

            migrationBuilder.DropTable(
                name: "CAT_Plazos");

            migrationBuilder.DropTable(
                name: "CAT_TasasIva");

            migrationBuilder.DropTable(
                name: "CAT_TasaValor");

            migrationBuilder.DropTable(
                name: "CAT_Tasa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Empresas",
                table: "CAT_Empresas");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Empresas_NomEmpresa",
                table: "CAT_Empresas");

            migrationBuilder.RenameTable(
                name: "CAT_Empresas",
                newName: "CAT_Empresa");

            migrationBuilder.AlterColumn<string>(
                name: "NomEmpresa",
                table: "CAT_Empresa",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Empresa",
                table: "CAT_Empresa",
                column: "Id");
        }
    }
}
