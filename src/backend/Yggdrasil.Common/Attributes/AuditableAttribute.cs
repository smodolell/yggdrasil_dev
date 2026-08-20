using System.ComponentModel;

namespace Yggdrasil.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AuditableAttribute(AuditEvents eventId) : Attribute
{
    public int EventId { get; } = (int)eventId;
}
public enum AuditEvents
{

    [Description("Creación Reporte")]
    CrearReporte = 1,
    [Description("Editar Reporte")]
    EditarReporte = 2,
    [Description("Eliminar Reporte")]
    EliminarReporte = 3,

    [Description("Creación Credito")]
    CrearCredito = 4,
    [Description("Editar Credito")]
    EditarCredito = 5,
    [Description("Activar Credito")]
    ActivarCredito = 6,

    [Description("Registrar Pago")]
    RegistrarPago = 7,
    [Description("Cancelar Pago")]
    CancelarPago = 8

}