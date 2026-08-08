using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yggdrasil.Common.Interfaces;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence;

public partial class ApplicationDbContext : IdentityDbContext<SYS_Usuario, SYS_Rol, int>, IApplicationDbContext
{
    public DbSet<CAT_Banco> CAT_Banco => Set<CAT_Banco>();
    public DbSet<CAT_CompaniaTelefonica> CAT_CompaniaTelefonica => Set<CAT_CompaniaTelefonica>();
    public DbSet<CAT_EdoCivil> CAT_EdoCivil => Set<CAT_EdoCivil>();
    public DbSet<CAT_Empresa> CAT_Empresa => Set<CAT_Empresa>();
    public DbSet<CAT_Genero> CAT_Genero => Set<CAT_Genero>();
    public DbSet<CAT_Moneda> CAT_Moneda => Set<CAT_Moneda>();
    public DbSet<CAT_Periodicidad> CAT_Periodicidad => Set<CAT_Periodicidad>();
    public DbSet<CAT_Plazo> CAT_Plazo => Set<CAT_Plazo>();
    public DbSet<CAT_Tasa> CAT_Tasa => Set<CAT_Tasa>();
    public DbSet<CAT_TasaIva> CAT_TasaIva => Set<CAT_TasaIva>();
    public DbSet<CAT_TasaValor> CAT_TasaValor => Set<CAT_TasaValor>();
    public DbSet<CAT_TipoDomicilio> CAT_TipoDomicilio => Set<CAT_TipoDomicilio>();
    public DbSet<CAT_TipoPersona> CAT_TipoPersona => Set<CAT_TipoPersona>();
    public DbSet<CAT_TipoTelefono> CAT_TipoTelefono => Set<CAT_TipoTelefono>();

    public DbSet<FI_Cargo> FI_Cargo => Set<FI_Cargo>();
    public DbSet<FI_CargoInicial> FI_CargoInicial => Set<FI_CargoInicial>();
    public DbSet<FI_ConceptoFinanciado> FI_ConceptoFinanciado => Set<FI_ConceptoFinanciado>();
    public DbSet<FI_Credito> FI_Credito => Set<FI_Credito>();
    public DbSet<FI_Domicilio> FI_Domicilio => Set<FI_Domicilio>();
    public DbSet<FI_EstatusCredito> FI_EstatusCredito => Set<FI_EstatusCredito>();
    public DbSet<FI_FormaPago> FI_FormaPago => Set<FI_FormaPago>();
    public DbSet<FI_Movimiento> FI_Movimiento => Set<FI_Movimiento>();
    public DbSet<FI_Pago> FI_Pago => Set<FI_Pago>();
    public DbSet<FI_PagoMovimiento> FI_PagoMovimiento => Set<FI_PagoMovimiento>();
    public DbSet<FI_Perfil> FI_Perfil => Set<FI_Perfil>();
    public DbSet<FI_PerfilSeccion> FI_PerfilSeccion => Set<FI_PerfilSeccion>();
    public DbSet<FI_Persona> FI_Persona => Set<FI_Persona>();
    public DbSet<FI_PersonaCuentaBancaria> FI_PersonaCuentaBancaria => Set<FI_PersonaCuentaBancaria>();
    public DbSet<FI_PersonaPerfil> FI_PersonaPerfil => Set<FI_PersonaPerfil>();
    public DbSet<FI_Producto> FI_Producto => Set<FI_Producto>();
    public DbSet<FI_Seccion> FI_Seccion => Set<FI_Seccion>();
    public DbSet<FI_TablaAmortiza> FI_TablaAmortiza => Set<FI_TablaAmortiza>();
    public DbSet<FI_Telefono> FI_Telefono => Set<FI_Telefono>();
    public DbSet<FI_TipoCalculo> FI_TipoCalculo => Set<FI_TipoCalculo>();
    public DbSet<FI_TipoMovimiento> FI_TipoMovimiento => Set<FI_TipoMovimiento>();
    public DbSet<FI_TipoPago> FI_TipoPago => Set<FI_TipoPago>();
    public DbSet<CAT_CalendarioLaboral> CAT_CalendarioLaboral => Set<CAT_CalendarioLaboral>();


    #region SYS_
    public DbSet<SYS_Audit> SYS_Audit => Set<SYS_Audit>();
    public DbSet<SYS_LoginLog> SYS_LoginLog => Set<SYS_LoginLog>();
    public DbSet<SYS_Consecutivo> SYS_Consecutivo => Set<SYS_Consecutivo>();


    public DbSet<SYS_AuditEvent> SYS_AuditEvent => Set<SYS_AuditEvent>();

    public DbSet<SYS_AccessPoint> SYS_AccessPoint => Set<SYS_AccessPoint>();
    public DbSet<SYS_AccessPointType> SYS_AccessPointType => Set<SYS_AccessPointType>();
    public DbSet<SYS_Application> SYS_Application => Set<SYS_Application>();
    public DbSet<SYS_Menu> SYS_Menu => Set<SYS_Menu>();
    public DbSet<SYS_Plugin> SYS_Plugin => Set<SYS_Plugin>();
    public DbSet<SYS_RolAccessPoint> SYS_RolAccessPoint => Set<SYS_RolAccessPoint>();




    #endregion


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


        var cascadeFKs = builder.Model.GetEntityTypes()
           .SelectMany(t => t.GetForeignKeys())
           .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
