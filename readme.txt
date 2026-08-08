================================================================================
 YGGDRASIL PROFUTURO - ARQUITECTURA DE LA APLICACION
================================================================================

Sistema financiero (creditos, cobranza, originacion) construido como un
MONOLITO MODULAR en .NET 10, con backend en ASP.NET Core Web API y frontend
en Blazor WebAssembly. La solucion se organiza en capas al estilo Clean
Architecture, y dentro de cada capa de negocio los modulos se organizan en
"vertical slices" (Features) usando CQRS.

--------------------------------------------------------------------------------
 1. ESTRUCTURA DE CARPETAS (mapea a Yggdrasil_Profuturo.slnx)
--------------------------------------------------------------------------------

Yggdrasil_Profuturo/
|
|-- src/backend/                      Backend (.NET 10)
|   |-- Yggdrasil.Domain/             Capa de dominio (entidades EF, sin dependencias)
|   |-- Yggdrasil.Common/             Capa "Application": interfaces, contratos,
|   |                                 extensiones, middlewares, tipos compartidos
|   |-- Yggdrasil.Infrastructure/     Implementaciones: EF Core DbContext,
|   |                                 migraciones, servicios de infraestructura
|   |-- Yggdrasil.ApiService/         Host ASP.NET Core (Program.cs), arma
|   |                                 y expone todos los modulos como una sola API
|   |-- modules/                      Modulos de negocio (plug-ins del backend)
|       |-- Yggdrasil.Module.Auth/
|       |-- Yggdrasil.Module.System/
|       |-- Yggdrasil.Module.Audit/
|       |-- Yggdrasil.Module.Catalog/
|       |-- Yggdrasil.Module.Cotizador/
|       |-- Yggdrasil.Module.Credito/
|       |-- Yggdrasil.Module.Cobranza/
|       |-- Yggdrasil.Module.Originacion/
|       |-- Yggdrasil.Module.Operacion/
|
|-- src/frontend/                     Frontend (.NET 10 / Blazor)
|   |-- Yggdrasil.Blazor/             Libreria compartida de UI: servicios HTTP,
|   |                                 auth, componentes base, helpers, DTOs
|   |-- Yggdrasil.WasmApp/            Host Blazor WebAssembly (Program.cs, App.razor)
|   |-- modules/                      Modulos de UI (uno por modulo de negocio)
|       |-- Yggdrasil.Module.Audit.UI/
|       |-- Yggdrasil.Module.Catalog.UI/
|       |-- Yggdrasil.Module.Layout.UI/
|       |-- Yggdrasil.Module.System.UI/
|
|-- tools/Yggdrasil.Tools/            CLI interno (Tharga.Console) para
|                                     scaffolding de modulos/contenedores nuevos
|                                     (comandos Build_ModulesCommand, Build_ContainerCommand)
|
|-- Yggdrasil_Profuturo.slnx          Solucion (formato .slnx de Visual Studio)

--------------------------------------------------------------------------------
 2. CAPAS DEL BACKEND (Clean Architecture)
--------------------------------------------------------------------------------

  Yggdrasil.Domain
    - Solo entidades EF (carpeta Entities/): CAT_*, FI_*, SYS_*.
    - Sin dependencias a otras capas del proyecto.

  Yggdrasil.Common  ("Application layer")
    - IApplicationDbContext: contrato de acceso a datos usado por los modulos
      (para no depender directamente de EF/Infrastructure).
    - IModule: contrato que implementa cada modulo de negocio para registrarse
      (Add -> servicios DI, Use -> middlewares/endpoints).
    - Extensions/, Handlers/, Middlewares/, Attributes/, DTOs/ compartidos.
    - Paquetes clave: LiteBus (Commands/Queries/Events -> patron CQRS),
      Ardalis.Specification (consultas encapsuladas), Ardalis.Result
      (resultados tipados), FluentValidation, Mapster (mapeo objeto-objeto).

  Yggdrasil.Infrastructure
    - ApplicationDbContext : IdentityDbContext<SYS_Usuario, SYS_Rol, int>,
      IApplicationDbContext  -> implementacion real con EF Core SQL Server.
    - Persistence/Configurations/  -> una clase IEntityTypeConfiguration<T>
      por entidad (Fluent API), aplicadas automaticamente por ensamblado.
    - Migrations/  -> migraciones de EF Core.
    - Services/    -> servicios de infraestructura (p.ej. generacion de
      consecutivos/folios).

  Yggdrasil.ApiService
    - Program.cs: arma el host ASP.NET Core.
      * AddYggdrasilApplication(...)  -> servicios base de la app
      * AddInfrastructure(...)        -> EF Core + DbContext
      * AddModules(assemblies...)     -> descubre e inyecta cada IModule
      * Autenticacion JWT Bearer, CORS, ProblemDetails, Swagger/Swashbuckle
        y Scalar (documentacion interactiva de la API en /scalar)
      * modules.Use(app) + app.MapEndpoints() + app.MapControllers()
        -> cada modulo registra sus propios endpoints minimal-API

--------------------------------------------------------------------------------
 3. MODULOS DE NEGOCIO (backend/modules)
--------------------------------------------------------------------------------

Cada modulo es un proyecto independiente que implementa IModule y se organiza
en "vertical slices" por caracteristica (Feature), no por tipo tecnico:

  Yggdrasil.Module.<Nombre>/
    |-- <Nombre>Module.cs        Implementa IModule (registro DI, Mapster, FluentValidation)
    |-- Endpoints/                Minimal API endpoints (GET/POST/PUT/DELETE)
    |-- Features/
        |-- <Area>/<Entidad>/
            |-- Commands/         Create/Update/Delete...Command (LiteBus ICommand)
            |-- Queries/          Get...Query (LiteBus IQuery)
            |-- DTOs/             DTOs + validadores FluentValidation
            |-- Specifications/   Especificaciones Ardalis.Specification (filtros/consultas)

Ejemplo real: Yggdrasil.Module.Credito
  Features/Configuracion/TipoMovimiento/{Commands,Queries,DTOs,Specifications}

Modulos existentes:
  - Auth         Autenticacion / login / JWT
  - System        Configuracion, menus, access points, aplicaciones, plugins
  - Audit         Bitacora de auditoria (SYS_Audit, SYS_AuditEvent)
  - Catalog       Catalogos generales (CAT_*)
  - Cotizador     Cotizacion de creditos
  - Credito       Motor de creditos (movimientos, tabla de amortizacion, etc.)
  - Cobranza      Cobranza / pagos
  - Originacion   Alta y originacion de creditos
  - Operacion     Operacion diaria

NOTA: no todos los modulos estan aun conectados en Yggdrasil.ApiService/Program.cs
(Auth, System, Catalog, Audit y Cotizador si; Credito/Cobranza/Originacion/
Operacion existen como proyectos pero su registro esta comentado o pendiente).

--------------------------------------------------------------------------------
 4. FRONTEND (Blazor WebAssembly)
--------------------------------------------------------------------------------

  Yggdrasil.WasmApp
    - Proyecto host (Microsoft.NET.Sdk.BlazorWebAssembly).
    - Referencia a Yggdrasil.Blazor y a todos los *.UI de frontend/modules.
    - Usa MudBlazor como libreria de componentes UI.

  Yggdrasil.Blazor
    - Libreria compartida: Auth/, Services/ (clientes HTTP hacia la API),
      Handlers/, Helpers/, Validation/, DTOs/, Components/ base.

  frontend/modules/Yggdrasil.Module.<Nombre>.UI
    - Un proyecto Razor Class Library por modulo de negocio (espejo del
      modulo de backend correspondiente): paginas, componentes y llamadas
      a los endpoints de ese modulo.
    - Modulos UI actuales: Audit, Catalog, Layout, System.

--------------------------------------------------------------------------------
 5. HERRAMIENTAS (tools/Yggdrasil.Tools)
--------------------------------------------------------------------------------

CLI de consola (Tharga.Console) usada para generar el andamiaje (scaffolding)
de nuevos modulos/contenedores de forma consistente con la arquitectura del
proyecto (Commands/, Templates/). No forma parte del runtime de la aplicacion.

--------------------------------------------------------------------------------
 6. STACK TECNOLOGICO
--------------------------------------------------------------------------------

  - .NET 10 / C#
  - ASP.NET Core Web API (minimal APIs + controllers) + Blazor WebAssembly
  - Entity Framework Core 10 (SQL Server) + ASP.NET Core Identity
  - LiteBus (Commands/Queries/Events) para CQRS
  - Ardalis.Specification, Ardalis.Result, Ardalis.GuardClauses
  - FluentValidation
  - Mapster (mapeo de objetos)
  - MudBlazor (componentes UI)
  - Swashbuckle / Scalar (documentacion OpenAPI)
  - Autenticacion JWT Bearer

--------------------------------------------------------------------------------
 7. FLUJO DE UNA PETICION (ejemplo tipico)
--------------------------------------------------------------------------------

  1. Frontend (Blazor) llama a un servicio HTTP en Yggdrasil.Blazor/Services.
  2. La peticion llega a un Endpoint minimal-API dentro de un modulo
     (backend/modules/<Modulo>/Endpoints).
  3. El endpoint despacha un Command o Query via LiteBus.
  4. El handler usa IApplicationDbContext (o una Specification de
     Ardalis.Specification) para leer/escribir contra ApplicationDbContext
     (Yggdrasil.Infrastructure), que persiste en SQL Server via EF Core.
  5. El resultado se mapea a un DTO (Mapster) y se devuelve como
     Ardalis.Result al frontend.

================================================================================
