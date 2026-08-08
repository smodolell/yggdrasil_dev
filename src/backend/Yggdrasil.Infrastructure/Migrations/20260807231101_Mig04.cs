using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yggdrasil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FI_PerfilId",
                table: "FI_Persona",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CAT_Banco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomBanco = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    CBUPrefix = table.Column<string>(type: "varchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    CodigoBCRA = table.Column<string>(type: "varchar(3)", fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Banco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FI_Perfil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomPerfil = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FI_Seccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomSeccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    IsCreate = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsEdit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsExtension = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Seccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FI_CuentaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<int>(type: "int", nullable: false),
                    NroCuentaBancaria = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CBU = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    AliasCBU = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_CuentaBancaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_CuentaBancaria_CAT_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "CAT_Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_CuentaBancaria_CAT_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "CAT_Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_PersonaCuentaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<int>(type: "int", nullable: false),
                    NroCuentaBancaria = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CBU = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    AliasCBU = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_PersonaCuentaBancaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_PersonaCuentaBancaria_CAT_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "CAT_Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_PersonaCuentaBancaria_CAT_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "CAT_Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_PersonaCuentaBancaria_FI_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "FI_Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_PersonaPerfil",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    PerfilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_PersonaPerfil", x => new { x.PersonaId, x.PerfilId });
                    table.ForeignKey(
                        name: "FK_FI_PersonaPerfil_FI_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "FI_Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_PersonaPerfil_FI_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "FI_Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_PerfilSeccion",
                columns: table => new
                {
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    SeccionId = table.Column<int>(type: "int", nullable: false),
                    ActivoCreate = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActivoEdit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActivoExtension = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_PerfilSeccion", x => new { x.PerfilId, x.SeccionId });
                    table.ForeignKey(
                        name: "FK_FI_PerfilSeccion_FI_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "FI_Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_PerfilSeccion_FI_Seccion_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "FI_Seccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FI_Persona_FI_PerfilId",
                table: "FI_Persona",
                column: "FI_PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Bancos_CBUPrefix",
                table: "CAT_Banco",
                column: "CBUPrefix");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Bancos_CodigoBCRA",
                table: "CAT_Banco",
                column: "CodigoBCRA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Bancos_NomBanco",
                table: "CAT_Banco",
                column: "NomBanco");

            migrationBuilder.CreateIndex(
                name: "IX_FI_CuentaBancaria_BancoId",
                table: "FI_CuentaBancaria",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_CuentaBancaria_MonedaId",
                table: "FI_CuentaBancaria",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_PerfilSeccion_SeccionId",
                table: "FI_PerfilSeccion",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_CuentasBancarias_BancoId",
                table: "FI_PersonaCuentaBancaria",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_CuentasBancarias_MonedaId",
                table: "FI_PersonaCuentaBancaria",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_PersonaCuentaBancaria_PersonaId",
                table: "FI_PersonaCuentaBancaria",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_PersonaPerfil_PerfilId",
                table: "FI_PersonaPerfil",
                column: "PerfilId");

            migrationBuilder.AddForeignKey(
                name: "FK_FI_Persona_FI_Perfil_FI_PerfilId",
                table: "FI_Persona",
                column: "FI_PerfilId",
                principalTable: "FI_Perfil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FI_Persona_FI_Perfil_FI_PerfilId",
                table: "FI_Persona");

            migrationBuilder.DropTable(
                name: "FI_CuentaBancaria");

            migrationBuilder.DropTable(
                name: "FI_PerfilSeccion");

            migrationBuilder.DropTable(
                name: "FI_PersonaCuentaBancaria");

            migrationBuilder.DropTable(
                name: "FI_PersonaPerfil");

            migrationBuilder.DropTable(
                name: "FI_Seccion");

            migrationBuilder.DropTable(
                name: "CAT_Banco");

            migrationBuilder.DropTable(
                name: "FI_Perfil");

            migrationBuilder.DropIndex(
                name: "IX_FI_Persona_FI_PerfilId",
                table: "FI_Persona");

            migrationBuilder.DropColumn(
                name: "FI_PerfilId",
                table: "FI_Persona");
        }
    }
}
