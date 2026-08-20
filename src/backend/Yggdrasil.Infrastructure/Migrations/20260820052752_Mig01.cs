using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yggdrasil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "CAT_CalendarioLaboral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    EsHabil = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CalendarioLaboral", x => x.Id);
                });

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
                name: "CAT_Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomEmpresa = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Empresas", x => x.Id);
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
                name: "CS_EstatusCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomEstatusCredito = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_EstatusCredito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_MetodoArmotizacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomMetodoArmotizacion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_MetodoArmotizacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_TipoMovimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    NomTipoMovimiento = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_TipoMovimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_TipoPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTipoPago = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_TipoPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DEV_CreditoIntraDia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MontoOtorgado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tasa = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TasaIva = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FechaPrimeraRenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEV_CreditoIntraDia", x => x.Id);
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
                name: "OT_Fase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ClaveFase = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    NomFase = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MapRoute = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EsInicial = table.Column<bool>(type: "bit", nullable: false),
                    EsFinal = table.Column<bool>(type: "bit", nullable: false),
                    InClient = table.Column<bool>(type: "bit", nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Orden = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Fase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RSP_Input",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomInput = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSP_Input", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RSP_Reporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomReporte = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    StoredProcedure = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReporteFormatoId = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSP_Reporte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_AccessPointTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AccessPointTypeName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_AccessPointTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ApplicationName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_AuditEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_AuditEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_Consecutivo",
                columns: table => new
                {
                    NombreTabla = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConsecutivoId = table.Column<int>(type: "int", nullable: false),
                    FecUltimoCambio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_Consecutivo", x => x.NombreTabla);
                });

            migrationBuilder.CreateTable(
                name: "SYS_LoginLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Agent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccessd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_LoginLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    Name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "CS_TipoCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaveTipoCredito = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NomTipoCredito = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Prefijo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Postfijo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Consecutivo = table.Column<int>(type: "int", nullable: false),
                    TipoMovimientoRentaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_TipoCredito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_TipoCredito_CS_TipoMovimiento_TipoMovimientoRentaId",
                        column: x => x.TipoMovimientoRentaId,
                        principalTable: "CS_TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CS_Pago",
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
                    table.PrimaryKey("PK_CS_Pago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_Pago_CS_TipoPago_TipoPagoId",
                        column: x => x.TipoPagoId,
                        principalTable: "CS_TipoPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DEV_InteresAcumulado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCalculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaldoCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dias = table.Column<int>(type: "int", nullable: false),
                    Tasa = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TasaIva = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Interes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEV_InteresAcumulado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEV_InteresAcumulado_DEV_CreditoIntraDia_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "DEV_CreditoIntraDia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DEV_MovimientoIntraDia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nro = table.Column<int>(type: "int", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoInsolutoResultante = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEV_MovimientoIntraDia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEV_MovimientoIntraDia_DEV_CreditoIntraDia_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "DEV_CreditoIntraDia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DEV_TablaAmortiza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    CreditoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEV_TablaAmortiza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEV_TablaAmortiza_DEV_CreditoIntraDia_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "DEV_CreditoIntraDia",
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
                    table.ForeignKey(
                        name: "FK_FI_Persona_FI_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "FI_Perfil",
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

            migrationBuilder.CreateTable(
                name: "OT_FaseEstado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaseId = table.Column<int>(type: "int", nullable: false),
                    NomEstado = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Inicial = table.Column<bool>(type: "bit", nullable: false),
                    Edicion = table.Column<bool>(type: "bit", nullable: false),
                    Completado = table.Column<bool>(type: "bit", nullable: false),
                    Rechazado = table.Column<bool>(type: "bit", nullable: false),
                    Condicionado = table.Column<bool>(type: "bit", nullable: false),
                    Espera = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_FaseEstado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OT_FaseEstado_OT_Fase_FaseId",
                        column: x => x.FaseId,
                        principalTable: "OT_Fase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RSP_Archivo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReporteId = table.Column<int>(type: "int", nullable: false),
                    LogParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NombreUnico = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MapPath = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSP_Archivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RSP_Archivo_RSP_Reporte_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "RSP_Reporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RSP_Parametro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReporteId = table.Column<int>(type: "int", nullable: false),
                    InputId = table.Column<int>(type: "int", nullable: false),
                    NomParametro = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    TipoDato = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TablaRef = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ColumnaValor = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ColumnaTexto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Display = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSP_Parametro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RSP_Parametro_RSP_Input_InputId",
                        column: x => x.InputId,
                        principalTable: "RSP_Input",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RSP_Parametro_RSP_Reporte_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "RSP_Reporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SYS_Plugins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    PluginName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    PluginDescription = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    MenuGlobal = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_Plugins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_Plugins_SYS_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "SYS_Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SYS_Audits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditEventId = table.Column<int>(type: "int", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    HasError = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_Audits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_Audits_SYS_AuditEvents_AuditEventId",
                        column: x => x.AuditEventId,
                        principalTable: "SYS_AuditEvents",
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
                name: "OT_Plan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    NomPlan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescPlan = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImporteMinimo = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    ImporteMaximo = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    GraciaCapital = table.Column<bool>(type: "bit", nullable: false),
                    GraciaInteres = table.Column<bool>(type: "bit", nullable: false),
                    TasaIvaConRFC = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    TasaIvaSinRFC = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    EdadMinima = table.Column<int>(type: "int", nullable: false),
                    EdadMaxima = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Plan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OT_Plan_FI_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "FI_Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CS_Credito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoCreditoId = table.Column<int>(type: "int", nullable: false),
                    EstatusCreditoId = table.Column<int>(type: "int", nullable: false),
                    PeriodicidadId = table.Column<int>(type: "int", nullable: false),
                    MetodoArmotizacionId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPrimeraRenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFirmaContrato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaActivacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClaveCredito = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Tasa = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    TasaIva = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Plazo = table.Column<int>(type: "int", nullable: false),
                    VersionTabla = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_Credito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_Credito_CAT_Periodicidades_PeriodicidadId",
                        column: x => x.PeriodicidadId,
                        principalTable: "CAT_Periodicidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CS_Credito_CS_EstatusCredito_EstatusCreditoId",
                        column: x => x.EstatusCreditoId,
                        principalTable: "CS_EstatusCredito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CS_Credito_CS_MetodoArmotizacion_MetodoArmotizacionId",
                        column: x => x.MetodoArmotizacionId,
                        principalTable: "CS_MetodoArmotizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CS_Credito_CS_TipoCredito_TipoCreditoId",
                        column: x => x.TipoCreditoId,
                        principalTable: "CS_TipoCredito",
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
                name: "SYS_AccessPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessPointTypeId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    PluginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    AccessPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescPageElement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_AccessPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_AccessPoints_SYS_AccessPointTypes_AccessPointTypeId",
                        column: x => x.AccessPointTypeId,
                        principalTable: "SYS_AccessPointTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYS_AccessPoints_SYS_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "SYS_Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYS_AccessPoints_SYS_Plugins_PluginId",
                        column: x => x.PluginId,
                        principalTable: "SYS_Plugins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_PlanFase",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    FaseId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_PlanFase", x => new { x.PlanId, x.FaseId });
                    table.ForeignKey(
                        name: "FK_OT_PlanFase_OT_Fase_FaseId",
                        column: x => x.FaseId,
                        principalTable: "OT_Fase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_PlanFase_OT_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "OT_Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_PlanPeriodicidad",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    PeriodicidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_PlanPeriodicidad", x => new { x.PlanId, x.PeriodicidadId });
                    table.ForeignKey(
                        name: "FK_OT_PlanPeriodicidad_CAT_Periodicidades_PeriodicidadId",
                        column: x => x.PeriodicidadId,
                        principalTable: "CAT_Periodicidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_PlanPeriodicidad_OT_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "OT_Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_PlanPlazo",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    ValorPlazo = table.Column<int>(type: "int", nullable: false),
                    PlazoId = table.Column<int>(type: "int", nullable: false),
                    TasaId = table.Column<int>(type: "int", nullable: false),
                    ValorTasa = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_PlanPlazo", x => new { x.PlanId, x.ValorPlazo });
                    table.ForeignKey(
                        name: "FK_OT_PlanPlazo_CAT_Plazos_PlazoId",
                        column: x => x.PlazoId,
                        principalTable: "CAT_Plazos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_PlanPlazo_CAT_Tasa_TasaId",
                        column: x => x.TasaId,
                        principalTable: "CAT_Tasa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_PlanPlazo_OT_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "OT_Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_Solicitud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaseEstadoId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    TipoPersonaId = table.Column<int>(type: "int", nullable: true),
                    BancoId = table.Column<int>(type: "int", nullable: true),
                    AsesorId = table.Column<int>(type: "int", nullable: true),
                    AnalistaId = table.Column<int>(type: "int", nullable: true),
                    SucursalId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImporteMinimo = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    ImporteMaximo = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    DestinoCredito = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MontoSolicitado = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    CBU = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Solicitud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OT_Solicitud_AspNetUsers_AnalistaId",
                        column: x => x.AnalistaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OT_Solicitud_AspNetUsers_AsesorId",
                        column: x => x.AsesorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OT_Solicitud_CAT_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "CAT_Banco",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OT_Solicitud_CAT_TipoPersona_TipoPersonaId",
                        column: x => x.TipoPersonaId,
                        principalTable: "CAT_TipoPersona",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OT_Solicitud_FI_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "FI_Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OT_Solicitud_OT_FaseEstado_FaseEstadoId",
                        column: x => x.FaseEstadoId,
                        principalTable: "OT_FaseEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Solicitud_OT_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "OT_Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CS_Movimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMovimientoId = table.Column<int>(type: "int", nullable: false),
                    CreditoId = table.Column<int>(type: "int", nullable: false),
                    DescMovimiento = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
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
                    table.PrimaryKey("PK_CS_Movimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_Movimiento_CS_Credito_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "CS_Credito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CS_Movimiento_CS_TipoMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "CS_TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CS_TablaAmortiza",
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
                    CreditoId = table.Column<int>(type: "int", nullable: false),
                    CS_CreditoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_TablaAmortiza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_TablaAmortiza_CS_Credito_CS_CreditoId",
                        column: x => x.CS_CreditoId,
                        principalTable: "CS_Credito",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CS_TablaAmortiza_CS_TipoMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "CS_TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CS_TablaAmortiza_FI_Credito_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "FI_Credito",
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
                name: "SYS_RolAccessPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    AccessPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_RolAccessPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_RolAccessPoints_AspNetRoles_RolId",
                        column: x => x.RolId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYS_RolAccessPoints_SYS_AccessPoints_AccessPointId",
                        column: x => x.AccessPointId,
                        principalTable: "SYS_AccessPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    TipoPersonaId = table.Column<int>(type: "int", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: true),
                    EdoCivilId = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CUIT = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaConstitucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TiempoResidenciaDomicilio = table.Column<short>(type: "smallint", nullable: false),
                    TiempoResidenciaCiudad = table.Column<short>(type: "smallint", nullable: false),
                    TelefonoCasa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TelefonoDomicilio = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TelefonoCelular = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "Date", nullable: true),
                    SostenFamiliar = table.Column<bool>(type: "bit", nullable: false),
                    DependientesEconomicos = table.Column<short>(type: "smallint", nullable: false),
                    NombreCompletoConyuge = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DNIConyuge = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TelefonoConyuge = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EmailConyuge = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EsSolicitante = table.Column<bool>(type: "bit", nullable: false),
                    EsBeneficiario = table.Column<bool>(type: "bit", nullable: false),
                    EsAval = table.Column<bool>(type: "bit", nullable: false),
                    EsObligadoSolidario = table.Column<bool>(type: "bit", nullable: false),
                    EsRepresentateLegal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OT_Persona_CAT_EdoCivil_EdoCivilId",
                        column: x => x.EdoCivilId,
                        principalTable: "CAT_EdoCivil",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OT_Persona_CAT_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "CAT_Generos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OT_Persona_CAT_TipoPersona_TipoPersonaId",
                        column: x => x.TipoPersonaId,
                        principalTable: "CAT_TipoPersona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Persona_OT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "OT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_SolicitudFase",
                columns: table => new
                {
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    FaseId = table.Column<int>(type: "int", nullable: false),
                    FaseEstadoId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OK = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_SolicitudFase", x => new { x.FaseId, x.SolicitudId });
                    table.ForeignKey(
                        name: "FK_OT_SolicitudFase_OT_FaseEstado_FaseEstadoId",
                        column: x => x.FaseEstadoId,
                        principalTable: "OT_FaseEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_SolicitudFase_OT_Fase_FaseId",
                        column: x => x.FaseId,
                        principalTable: "OT_Fase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_SolicitudFase_OT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "OT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CS_PagoMovimiento",
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
                    table.PrimaryKey("PK_CS_PagoMovimiento", x => new { x.PagoId, x.MovimientoId });
                    table.ForeignKey(
                        name: "FK_CS_PagoMovimiento_CS_Movimiento_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "CS_Movimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CS_PagoMovimiento_CS_Pago_PagoId",
                        column: x => x.PagoId,
                        principalTable: "CS_Pago",
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
                table: "CS_EstatusCredito",
                columns: new[] { "Id", "Activo", "NomEstatusCredito" },
                values: new object[,]
                {
                    { 1, true, "CAPTURADO" },
                    { 2, true, "ACTIVO" },
                    { 3, true, "TERMINADO" }
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

            migrationBuilder.InsertData(
                table: "RSP_Input",
                columns: new[] { "Id", "NomInput" },
                values: new object[,]
                {
                    { 1, "TextBox" },
                    { 2, "CheckBox" },
                    { 3, "TextBoxDatepicker" },
                    { 4, "DropDownList" }
                });

            migrationBuilder.InsertData(
                table: "SYS_AccessPointTypes",
                columns: new[] { "Id", "AccessPointTypeName" },
                values: new object[,]
                {
                    { 0, "LeftMenu" },
                    { 1, "Page" },
                    { 2, "Element" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NombreCompleto",
                table: "AspNetUsers",
                column: "NombreCompleto");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_PSV_CalendarioLaboral_Fecha",
                table: "CAT_CalendarioLaboral",
                column: "Fecha",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_EdosCivil_NomEdoCivil",
                table: "CAT_EdoCivil",
                column: "NomEdoCivil",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Empresas_NomEmpresa",
                table: "CAT_Empresas",
                column: "NomEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Generos_NomGenero",
                table: "CAT_Generos",
                column: "NomGenero",
                unique: true);

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
                name: "IX_CS_Credito_EstatusCreditoId",
                table: "CS_Credito",
                column: "EstatusCreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_Credito_MetodoArmotizacionId",
                table: "CS_Credito",
                column: "MetodoArmotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_Credito_PeriodicidadId",
                table: "CS_Credito",
                column: "PeriodicidadId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_Credito_TipoCreditoId",
                table: "CS_Credito",
                column: "TipoCreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_Movimiento_CreditoId",
                table: "CS_Movimiento",
                column: "CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_Movimiento_TipoMovimientoId",
                table: "CS_Movimiento",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_Pago_TipoPagoId",
                table: "CS_Pago",
                column: "TipoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_PagoMovimiento_MovimientoId",
                table: "CS_PagoMovimiento",
                column: "MovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_TablaAmortiza_CreditoId",
                table: "CS_TablaAmortiza",
                column: "CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_TablaAmortiza_CS_CreditoId",
                table: "CS_TablaAmortiza",
                column: "CS_CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_TablaAmortiza_TipoMovimientoId",
                table: "CS_TablaAmortiza",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_TipoCredito_TipoMovimientoRentaId",
                table: "CS_TipoCredito",
                column: "TipoMovimientoRentaId");

            migrationBuilder.CreateIndex(
                name: "IX_DEV_InteresAcumulado_CreditoId",
                table: "DEV_InteresAcumulado",
                column: "CreditoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DEV_MovimientoIntraDia_CreditoId",
                table: "DEV_MovimientoIntraDia",
                column: "CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_DEV_TablaAmortiza_CreditoId",
                table: "DEV_TablaAmortiza",
                column: "CreditoId");

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
                name: "IX_FI_CuentaBancaria_BancoId",
                table: "FI_CuentaBancaria",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_FI_CuentaBancaria_MonedaId",
                table: "FI_CuentaBancaria",
                column: "MonedaId");

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
                name: "IX_FI_PerfilSeccion_SeccionId",
                table: "FI_PerfilSeccion",
                column: "SeccionId");

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

            migrationBuilder.CreateIndex(
                name: "IX_OT_FaseEstado_FaseId",
                table: "OT_FaseEstado",
                column: "FaseId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Persona_EdoCivilId",
                table: "OT_Persona",
                column: "EdoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Persona_GeneroId",
                table: "OT_Persona",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Persona_SolicitudId",
                table: "OT_Persona",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Persona_TipoPersonaId",
                table: "OT_Persona",
                column: "TipoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Plan_ProductoId",
                table: "OT_Plan",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_PlanFase_FaseId",
                table: "OT_PlanFase",
                column: "FaseId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_PlanPeriodicidad_PeriodicidadId",
                table: "OT_PlanPeriodicidad",
                column: "PeriodicidadId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_PlanPlazo_PlazoId",
                table: "OT_PlanPlazo",
                column: "PlazoId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_PlanPlazo_TasaId",
                table: "OT_PlanPlazo",
                column: "TasaId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Solicitud_AnalistaId",
                table: "OT_Solicitud",
                column: "AnalistaId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Solicitud_AsesorId",
                table: "OT_Solicitud",
                column: "AsesorId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Solicitud_BancoId",
                table: "OT_Solicitud",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Solicitud_FaseEstadoId",
                table: "OT_Solicitud",
                column: "FaseEstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Solicitud_PlanId",
                table: "OT_Solicitud",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Solicitud_ProductoId",
                table: "OT_Solicitud",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Solicitud_TipoPersonaId",
                table: "OT_Solicitud",
                column: "TipoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_SolicitudFase_FaseEstadoId",
                table: "OT_SolicitudFase",
                column: "FaseEstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_OT_SolicitudFase_SolicitudId",
                table: "OT_SolicitudFase",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_RSP_Archivo_ReporteId",
                table: "RSP_Archivo",
                column: "ReporteId");

            migrationBuilder.CreateIndex(
                name: "IX_RSP_Parametro_InputId",
                table: "RSP_Parametro",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_RSP_Parametro_ReporteId",
                table: "RSP_Parametro",
                column: "ReporteId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPoints_AccessPointTypeId",
                table: "SYS_AccessPoints",
                column: "AccessPointTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPoints_ApplicationId",
                table: "SYS_AccessPoints",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPoints_MenuId",
                table: "SYS_AccessPoints",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPoints_PluginId",
                table: "SYS_AccessPoints",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPointTypes_AccessPointTypeName",
                table: "SYS_AccessPointTypes",
                column: "AccessPointTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Applications_ApplicationName",
                table: "SYS_Applications",
                column: "ApplicationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AuditEvents_Description",
                table: "SYS_AuditEvents",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Audits_AuditEventId",
                table: "SYS_Audits",
                column: "AuditEventId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Audits_RegisteredDate",
                table: "SYS_Audits",
                column: "RegisteredDate");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Audits_UserName",
                table: "SYS_Audits",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Menus_Name",
                table: "SYS_Menus",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Plugins_ApplicationId",
                table: "SYS_Plugins",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_Plugins_PluginName",
                table: "SYS_Plugins",
                column: "PluginName");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_RolAccessPoints_AccessPointId",
                table: "SYS_RolAccessPoints",
                column: "AccessPointId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_RolAccessPoints_RolId_AccessPointId",
                table: "SYS_RolAccessPoints",
                columns: new[] { "RolId", "AccessPointId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CAT_CalendarioLaboral");

            migrationBuilder.DropTable(
                name: "CAT_TasasIva");

            migrationBuilder.DropTable(
                name: "CAT_TasaValor");

            migrationBuilder.DropTable(
                name: "CS_PagoMovimiento");

            migrationBuilder.DropTable(
                name: "CS_TablaAmortiza");

            migrationBuilder.DropTable(
                name: "DEV_InteresAcumulado");

            migrationBuilder.DropTable(
                name: "DEV_MovimientoIntraDia");

            migrationBuilder.DropTable(
                name: "DEV_TablaAmortiza");

            migrationBuilder.DropTable(
                name: "FI_CargoInicial");

            migrationBuilder.DropTable(
                name: "FI_ConceptoFinanciado");

            migrationBuilder.DropTable(
                name: "FI_CuentaBancaria");

            migrationBuilder.DropTable(
                name: "FI_Domicilio");

            migrationBuilder.DropTable(
                name: "FI_PagoMovimiento");

            migrationBuilder.DropTable(
                name: "FI_PerfilSeccion");

            migrationBuilder.DropTable(
                name: "FI_PersonaCuentaBancaria");

            migrationBuilder.DropTable(
                name: "FI_PersonaPerfil");

            migrationBuilder.DropTable(
                name: "FI_TablaAmortiza");

            migrationBuilder.DropTable(
                name: "FI_Telefono");

            migrationBuilder.DropTable(
                name: "OT_Persona");

            migrationBuilder.DropTable(
                name: "OT_PlanFase");

            migrationBuilder.DropTable(
                name: "OT_PlanPeriodicidad");

            migrationBuilder.DropTable(
                name: "OT_PlanPlazo");

            migrationBuilder.DropTable(
                name: "OT_SolicitudFase");

            migrationBuilder.DropTable(
                name: "RSP_Archivo");

            migrationBuilder.DropTable(
                name: "RSP_Parametro");

            migrationBuilder.DropTable(
                name: "SYS_Audits");

            migrationBuilder.DropTable(
                name: "SYS_Consecutivo");

            migrationBuilder.DropTable(
                name: "SYS_LoginLog");

            migrationBuilder.DropTable(
                name: "SYS_RolAccessPoints");

            migrationBuilder.DropTable(
                name: "CS_Movimiento");

            migrationBuilder.DropTable(
                name: "CS_Pago");

            migrationBuilder.DropTable(
                name: "DEV_CreditoIntraDia");

            migrationBuilder.DropTable(
                name: "FI_Cargo");

            migrationBuilder.DropTable(
                name: "CAT_TipoDomicilio");

            migrationBuilder.DropTable(
                name: "FI_Movimiento");

            migrationBuilder.DropTable(
                name: "FI_Pago");

            migrationBuilder.DropTable(
                name: "FI_Seccion");

            migrationBuilder.DropTable(
                name: "CAT_CompaniaTelefonica");

            migrationBuilder.DropTable(
                name: "CAT_TiposTelefono");

            migrationBuilder.DropTable(
                name: "CAT_Plazos");

            migrationBuilder.DropTable(
                name: "CAT_Tasa");

            migrationBuilder.DropTable(
                name: "OT_Solicitud");

            migrationBuilder.DropTable(
                name: "RSP_Input");

            migrationBuilder.DropTable(
                name: "RSP_Reporte");

            migrationBuilder.DropTable(
                name: "SYS_AuditEvents");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "SYS_AccessPoints");

            migrationBuilder.DropTable(
                name: "CS_Credito");

            migrationBuilder.DropTable(
                name: "CS_TipoPago");

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
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CAT_Banco");

            migrationBuilder.DropTable(
                name: "OT_FaseEstado");

            migrationBuilder.DropTable(
                name: "OT_Plan");

            migrationBuilder.DropTable(
                name: "SYS_AccessPointTypes");

            migrationBuilder.DropTable(
                name: "SYS_Menus");

            migrationBuilder.DropTable(
                name: "SYS_Plugins");

            migrationBuilder.DropTable(
                name: "CS_EstatusCredito");

            migrationBuilder.DropTable(
                name: "CS_MetodoArmotizacion");

            migrationBuilder.DropTable(
                name: "CS_TipoCredito");

            migrationBuilder.DropTable(
                name: "CAT_Periodicidades");

            migrationBuilder.DropTable(
                name: "FI_EstatusCredito");

            migrationBuilder.DropTable(
                name: "FI_Persona");

            migrationBuilder.DropTable(
                name: "OT_Fase");

            migrationBuilder.DropTable(
                name: "FI_Producto");

            migrationBuilder.DropTable(
                name: "SYS_Applications");

            migrationBuilder.DropTable(
                name: "CS_TipoMovimiento");

            migrationBuilder.DropTable(
                name: "CAT_EdoCivil");

            migrationBuilder.DropTable(
                name: "CAT_Generos");

            migrationBuilder.DropTable(
                name: "CAT_TipoPersona");

            migrationBuilder.DropTable(
                name: "FI_Perfil");

            migrationBuilder.DropTable(
                name: "CAT_Empresas");

            migrationBuilder.DropTable(
                name: "CAT_Monedas");
        }
    }
}
