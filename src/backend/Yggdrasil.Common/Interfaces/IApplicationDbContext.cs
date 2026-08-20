using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Common.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }

    DbSet<CAT_Banco> CAT_Banco { get; }
    DbSet<CAT_CompaniaTelefonica> CAT_CompaniaTelefonica { get; }
    DbSet<CAT_EdoCivil> CAT_EdoCivil { get; }
    DbSet<CAT_Empresa> CAT_Empresa { get; }
    DbSet<CAT_Genero> CAT_Genero { get; }
    DbSet<CAT_Moneda> CAT_Moneda { get; }
    DbSet<CAT_Periodicidad> CAT_Periodicidad { get; }
    DbSet<CAT_Plazo> CAT_Plazo { get; }
    DbSet<CAT_Tasa> CAT_Tasa { get; }
    DbSet<CAT_TasaIva> CAT_TasaIva { get; }
    DbSet<CAT_TipoDomicilio> CAT_TipoDomicilio { get; }
    DbSet<CAT_TipoPersona> CAT_TipoPersona { get; }
    DbSet<CAT_TipoTelefono> CAT_TipoTelefono { get; }

    DbSet<FI_Cargo> FI_Cargo { get; }
    DbSet<FI_CargoInicial> FI_CargoInicial { get; }
    DbSet<FI_ConceptoFinanciado> FI_ConceptoFinanciado { get; }
    DbSet<FI_Credito> FI_Credito { get; }
    DbSet<FI_PersonaCuentaBancaria> FI_PersonaCuentaBancaria { get; }
    DbSet<FI_PersonaPerfil> FI_PersonaPerfil { get; }
    DbSet<FI_Domicilio> FI_Domicilio { get; }
    DbSet<FI_EstatusCredito> FI_EstatusCredito { get; }
    DbSet<FI_FormaPago> FI_FormaPago { get; }
    DbSet<FI_Movimiento> FI_Movimiento { get; }
    DbSet<FI_Pago> FI_Pago { get; }
    DbSet<FI_PagoMovimiento> FI_PagoMovimiento { get; }
    DbSet<FI_Perfil> FI_Perfil { get; }
    DbSet<FI_PerfilSeccion> FI_PerfilSeccion { get; }
    DbSet<FI_Persona> FI_Persona { get; }
    DbSet<FI_Producto> FI_Producto { get; }
    DbSet<FI_Seccion> FI_Seccion { get; }
    DbSet<FI_TablaAmortiza> FI_TablaAmortiza { get; }
    DbSet<FI_Telefono> FI_Telefono { get; }
    DbSet<FI_TipoCalculo> FI_TipoCalculo { get; }
    DbSet<FI_TipoMovimiento> FI_TipoMovimiento { get; }
    DbSet<FI_TipoPago> FI_TipoPago { get; }


    DbSet<CAT_TasaValor> CAT_TasaValor { get; }
    DbSet<SYS_AccessPoint> SYS_AccessPoint { get; }
    DbSet<SYS_AccessPointType> SYS_AccessPointType { get; }
    DbSet<SYS_RolAccessPoint> SYS_RolAccessPoint { get; }
    DbSet<SYS_Menu> SYS_Menu { get; }
    DbSet<SYS_Audit> SYS_Audit { get; }
    DbSet<SYS_AuditEvent> SYS_AuditEvent { get; }
    DbSet<SYS_LoginLog> SYS_LoginLog { get; }

    DbSet<SYS_Application> SYS_Application { get; }
    DbSet<SYS_Plugin> SYS_Plugin { get; }
    DbSet<SYS_Consecutivo> SYS_Consecutivo { get; }
    DbSet<SYS_Rol> Roles { get; }
    DbSet<CAT_CalendarioLaboral> CAT_CalendarioLaboral { get; }


    DbSet<RSP_Archivo> RSP_Archivo { get; }
    DbSet<RSP_Input> RSP_Input { get; }
    DbSet<RSP_Parametro> RSP_Parametro { get; }
    DbSet<RSP_Reporte> RSP_Reporte { get; }
    DbSet<DEV_CreditoIntraDia> DEV_CreditoIntraDia { get; }
    DbSet<DEV_InteresAcumulado> DEV_InteresAcumulado { get; }
    DbSet<DEV_MovimientoIntraDia> DEV_MovimientoIntraDia { get; }
    DbSet<DEV_TablaAmortiza> DEV_TablaAmortiza { get; }




    #region OT
    DbSet<OT_Plan> OT_Plan { get; }
    DbSet<OT_PlanPeriodicidad> OT_PlanPeriodicidad { get; }

    #endregion

    #region CS_
    DbSet<CS_Credito> CS_Credito { get; }
    DbSet<CS_EstatusCredito> CS_EstatusCredito { get; }
    DbSet<CS_MetodoArmotizacion> CS_MetodoArmotizacion { get; }
    DbSet<CS_Movimiento> CS_Movimiento { get; }
    DbSet<CS_Pago> CS_Pago { get; }
    DbSet<CS_PagoMovimiento> CS_PagoMovimiento { get; }
    DbSet<CS_TablaAmortiza> CS_TablaAmortiza { get; }
    DbSet<CS_TipoCredito> CS_TipoCredito { get; }
    DbSet<CS_TipoMovimiento> CS_TipoMovimiento { get; }
    DbSet<CS_TipoPago> CS_TipoPago { get; }

    #endregion
    IApplicationDbContextProcedures Procedures { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
