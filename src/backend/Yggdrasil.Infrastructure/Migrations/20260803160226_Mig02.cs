using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yggdrasil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_CompaniaTelefonica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomCompaniaTelefonica = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CompaniaTelefonica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_EdoCivil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomEdoCivil = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_EdoCivil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomGenero = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TipoDomicilio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomTipoDomicilio = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TipoDomicilio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TipoPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTipoPago = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TipoPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TipoPersona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomTipoPersona = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TipoPersona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TiposTelefono",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomTipoTelefono = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TiposTelefono", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FI_EstatusCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomEstatusCredito = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_EstatusCredito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FI_FormaPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomFormaPago = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_FormaPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FI_Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<int>(type: "int", nullable: false),
                    ClaveProducto = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NomProducto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Posfijo = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Prefijo = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Consecutivo = table.Column<int>(type: "int", nullable: false),
                    TipoMovimientoRentaId = table.Column<int>(type: "int", nullable: false),
                    TipoMovimientoMoraId = table.Column<int>(type: "int", nullable: false),
                    TasaMoraDefault = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    MoraPeriodoGracia = table.Column<int>(type: "int", nullable: false),
                    FactorTasaMora = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_Producto_CAT_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "CAT_Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Producto_CAT_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "CAT_Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_TipoCalculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomTipoCalculo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EsCargoInicial = table.Column<bool>(type: "bit", nullable: false),
                    EsConceptoFinanciado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_TipoCalculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FI_TipoMovimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    NomTipoMovimiento = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    GeneraIvaCapital = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GeneraIvaInteres = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GeneraMora = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EsCargoInicial = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EsConceptoFinanciado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_TipoMovimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FI_Pago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPagoId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    SaldoFavor = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Cancelado = table.Column<bool>(type: "bit", nullable: false),
                    Suspenso = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Pago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_Pago_CAT_TipoPago_TipoPagoId",
                        column: x => x.TipoPagoId,
                        principalTable: "CAT_TipoPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificador = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    TipoPersonaId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    EdoCivilId = table.Column<int>(type: "int", nullable: false),
                    LugarNacimientoId = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CURP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NSS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FechaConstitucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaAltaCliente = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_Persona_CAT_EdoCivil_EdoCivilId",
                        column: x => x.EdoCivilId,
                        principalTable: "CAT_EdoCivil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Persona_CAT_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "CAT_Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Persona_CAT_TipoPersona_TipoPersonaId",
                        column: x => x.TipoPersonaId,
                        principalTable: "CAT_TipoPersona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    TipoMovimientoId = table.Column<int>(type: "int", nullable: false),
                    TipoCalculoId = table.Column<int>(type: "int", nullable: false),
                    FormaPagoId = table.Column<int>(type: "int", nullable: true),
                    Concepto = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    EquivaleNroPeriodos = table.Column<int>(type: "int", nullable: false),
                    EsCargoInicial = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EsConceptoFinanciado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PermiteEdicion = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Cargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_Cargo_FI_FormaPago_FormaPagoId",
                        column: x => x.FormaPagoId,
                        principalTable: "FI_FormaPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Cargo_FI_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "FI_Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Cargo_FI_TipoCalculo_TipoCalculoId",
                        column: x => x.TipoCalculoId,
                        principalTable: "FI_TipoCalculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Cargo_FI_TipoMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "FI_TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_Credito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    EstatusCreditoId = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaveCredito = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    CapitalFinanciado = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPrimeraRenta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaActivacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaTerminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tasa = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    PuntosMas = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    PuntosPor = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    TasaBase = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    TasaMora = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    PuntosMasMora = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    PuntosPorMora = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    TasaBaseMora = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    TasaIva = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Plazo = table.Column<int>(type: "int", nullable: false),
                    PeriodicidadId = table.Column<int>(type: "int", nullable: false),
                    VersionTabla = table.Column<int>(type: "int", nullable: false),
                    PagoMensual = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Credito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_Credito_CAT_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "CAT_Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Credito_CAT_Periodicidades_PeriodicidadId",
                        column: x => x.PeriodicidadId,
                        principalTable: "CAT_Periodicidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Credito_FI_EstatusCredito_EstatusCreditoId",
                        column: x => x.EstatusCreditoId,
                        principalTable: "FI_EstatusCredito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Credito_FI_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "FI_Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Credito_FI_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "FI_Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_Domicilio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    TipoDomicilioId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Calle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Piso = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    EntreCalles = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    YCalle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FI_PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Domicilio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_Domicilio_CAT_TipoDomicilio_TipoDomicilioId",
                        column: x => x.TipoDomicilioId,
                        principalTable: "CAT_TipoDomicilio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Domicilio_FI_Persona_FI_PersonaId",
                        column: x => x.FI_PersonaId,
                        principalTable: "FI_Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Domicilio_FI_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "FI_Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_Telefono",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoTelefonoId = table.Column<int>(type: "int", nullable: false),
                    CompaniaTelefonicaId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InfoAdicional = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FI_PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Telefono", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_Telefono_CAT_CompaniaTelefonica_CompaniaTelefonicaId",
                        column: x => x.CompaniaTelefonicaId,
                        principalTable: "CAT_CompaniaTelefonica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Telefono_CAT_TiposTelefono_TipoTelefonoId",
                        column: x => x.TipoTelefonoId,
                        principalTable: "CAT_TiposTelefono",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Telefono_FI_Persona_FI_PersonaId",
                        column: x => x.FI_PersonaId,
                        principalTable: "FI_Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Telefono_FI_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "FI_Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_CargoInicial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditoId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    TipoMovimientoId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_CargoInicial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_CargoInicial_FI_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "FI_Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_CargoInicial_FI_Credito_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "FI_Credito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_CargoInicial_FI_TipoMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "FI_TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_ConceptoFinanciado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditoId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    TipoMovimientoId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_ConceptoFinanciado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_ConceptoFinanciado_FI_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "FI_Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_ConceptoFinanciado_FI_Credito_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "FI_Credito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_ConceptoFinanciado_FI_TipoMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "FI_TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_Movimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMovimientoId = table.Column<int>(type: "int", nullable: false),
                    CreditoId = table.Column<int>(type: "int", nullable: false),
                    DescMovimiento = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Interes = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    SaldoCapital = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    SaldoInteres = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    SaldoIva = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    SaldoTotal = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    NoPago = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_Movimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_Movimiento_FI_Credito_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "FI_Credito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_Movimiento_FI_TipoMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "FI_TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_TablaAmortiza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoMovimientoId = table.Column<int>(type: "int", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoPago = table.Column<int>(type: "int", nullable: false),
                    Dias = table.Column<int>(type: "int", nullable: false),
                    SaldoInicial = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Interes = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    SaldoFinal = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    TasaCalculo = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Procesado = table.Column<bool>(type: "bit", nullable: false),
                    VersionTabla = table.Column<int>(type: "int", nullable: false),
                    CreditoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_TablaAmortiza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FI_TablaAmortiza_FI_Credito_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "FI_Credito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_TablaAmortiza_FI_TipoMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "FI_TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FI_PagoMovimiento",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "int", nullable: false),
                    MovimientoId = table.Column<int>(type: "int", nullable: false),
                    TotalPagado = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    CapitalPagado = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    InteresPagado = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    IvaPagado = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cancelado = table.Column<bool>(type: "bit", nullable: false),
                    MotivoCancelacion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FI_PagoMovimiento", x => new { x.PagoId, x.MovimientoId });
                    table.ForeignKey(
                        name: "FK_FI_PagoMovimiento_FI_Movimiento_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "FI_Movimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FI_PagoMovimiento_FI_Pago_PagoId",
                        column: x => x.PagoId,
                        principalTable: "FI_Pago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CAT_EdoCivil",
                columns: new[] { "Id", "NomEdoCivil" },
                values: new object[,]
                {
                    { 1, "Desconocido" },
                    { 2, "Soltero" },
                    { 3, "Casado" },
                    { 4, "Viudo" },
                    { 5, "Divorciado" },
                    { 6, "Unión Libre" },
                    { 7, "Comprometido" }
                });

            migrationBuilder.InsertData(
                table: "CAT_Generos",
                columns: new[] { "Id", "NomGenero" },
                values: new object[,]
                {
                    { 1, "FEMENINO" },
                    { 2, "MASCULINO" }
                });

            migrationBuilder.InsertData(
                table: "CAT_TipoPersona",
                columns: new[] { "Id", "Activo", "NomTipoPersona" },
                values: new object[,]
                {
                    { 1, false, "Persona Física" },
                    { 2, false, "Persona Moral" }
                });

            migrationBuilder.InsertData(
                table: "FI_EstatusCredito",
                columns: new[] { "Id", "NomEstatusCredito" },
                values: new object[,]
                {
                    { 1, "CAPTURADO" },
                    { 2, "ACTIVO" },
                    { 3, "TERMINADO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_EdosCivil_NomEdoCivil",
                table: "CAT_EdoCivil",
                column: "NomEdoCivil",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Generos_NomGenero",
                table: "CAT_Generos",
                column: "NomGenero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TiposDomicilio_NomTipoDomicilio",
                table: "CAT_TipoDomicilio",
                column: "NomTipoDomicilio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TiposPersona_NomTipoPersona",
                table: "CAT_TipoPersona",
                column: "NomTipoPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TiposTelefono_NomTipoTelefono",
                table: "CAT_TiposTelefono",
                column: "NomTipoTelefono",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FI_Cargo_FormaPagoId",
                table: "FI_Cargo",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Cargo_ProductoId",
                table: "FI_Cargo",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Cargo_TipoCalculoId",
                table: "FI_Cargo",
                column: "TipoCalculoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Cargo_TipoMovimientoId",
                table: "FI_Cargo",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_CargoInicial_CargoId",
                table: "FI_CargoInicial",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_CargoInicial_CreditoId",
                table: "FI_CargoInicial",
                column: "CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_CargoInicial_TipoMovimientoId",
                table: "FI_CargoInicial",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_ConceptoFinanciado_CargoId",
                table: "FI_ConceptoFinanciado",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_ConceptoFinanciado_CreditoId",
                table: "FI_ConceptoFinanciado",
                column: "CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_ConceptoFinanciado_TipoMovimientoId",
                table: "FI_ConceptoFinanciado",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Credito_ClaveCredito",
                table: "FI_Credito",
                column: "ClaveCredito",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FI_Credito_EstatusCreditoId",
                table: "FI_Credito",
                column: "EstatusCreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Credito_MonedaId",
                table: "FI_Credito",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Credito_PeriodicidadId",
                table: "FI_Credito",
                column: "PeriodicidadId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Credito_PersonaId",
                table: "FI_Credito",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Credito_ProductoId",
                table: "FI_Credito",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Domicilio_FI_PersonaId",
                table: "FI_Domicilio",
                column: "FI_PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Domicilio_PersonaId",
                table: "FI_Domicilio",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Domicilio_TipoDomicilioId",
                table: "FI_Domicilio",
                column: "TipoDomicilioId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Movimiento_CreditoId_FechaVencimiento",
                table: "FI_Movimiento",
                columns: new[] { "CreditoId", "FechaVencimiento" });

            migrationBuilder.CreateIndex(
                name: "IX_FI_Movimiento_TipoMovimientoId",
                table: "FI_Movimiento",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Pago_TipoPagoId",
                table: "FI_Pago",
                column: "TipoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_PagoMovimiento_MovimientoId",
                table: "FI_PagoMovimiento",
                column: "MovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Persona_EdoCivilId",
                table: "FI_Persona",
                column: "EdoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_CURP",
                table: "FI_Persona",
                column: "CURP");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_Email",
                table: "FI_Persona",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_GeneroId",
                table: "FI_Persona",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_PerfilId",
                table: "FI_Persona",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_RFC",
                table: "FI_Persona",
                column: "RFC");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_TipoPersonaId",
                table: "FI_Persona",
                column: "TipoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Producto_EmpresaId",
                table: "FI_Producto",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Producto_MonedaId",
                table: "FI_Producto",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_TablaAmortiza_CreditoId",
                table: "FI_TablaAmortiza",
                column: "CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_TablaAmortiza_TipoMovimientoId",
                table: "FI_TablaAmortiza",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Telefono_CompaniaTelefonicaId",
                table: "FI_Telefono",
                column: "CompaniaTelefonicaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Telefono_FI_PersonaId",
                table: "FI_Telefono",
                column: "FI_PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Telefono_PersonaId",
                table: "FI_Telefono",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_Telefono_TipoTelefonoId",
                table: "FI_Telefono",
                column: "TipoTelefonoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TipoMovimiento_Clave",
                table: "FI_TipoMovimiento",
                column: "Clave",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FI_CargoInicial");

            migrationBuilder.DropTable(
                name: "FI_ConceptoFinanciado");

            migrationBuilder.DropTable(
                name: "FI_Domicilio");

            migrationBuilder.DropTable(
                name: "FI_PagoMovimiento");

            migrationBuilder.DropTable(
                name: "FI_TablaAmortiza");

            migrationBuilder.DropTable(
                name: "FI_Telefono");

            migrationBuilder.DropTable(
                name: "FI_Cargo");

            migrationBuilder.DropTable(
                name: "CAT_TipoDomicilio");

            migrationBuilder.DropTable(
                name: "FI_Movimiento");

            migrationBuilder.DropTable(
                name: "FI_Pago");

            migrationBuilder.DropTable(
                name: "CAT_CompaniaTelefonica");

            migrationBuilder.DropTable(
                name: "CAT_TiposTelefono");

            migrationBuilder.DropTable(
                name: "FI_FormaPago");

            migrationBuilder.DropTable(
                name: "FI_TipoCalculo");

            migrationBuilder.DropTable(
                name: "FI_Credito");

            migrationBuilder.DropTable(
                name: "FI_TipoMovimiento");

            migrationBuilder.DropTable(
                name: "CAT_TipoPago");

            migrationBuilder.DropTable(
                name: "FI_EstatusCredito");

            migrationBuilder.DropTable(
                name: "FI_Persona");

            migrationBuilder.DropTable(
                name: "FI_Producto");

            migrationBuilder.DropTable(
                name: "CAT_EdoCivil");

            migrationBuilder.DropTable(
                name: "CAT_Generos");

            migrationBuilder.DropTable(
                name: "CAT_TipoPersona");
        }
    }
}
