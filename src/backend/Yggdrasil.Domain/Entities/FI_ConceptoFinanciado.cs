namespace Yggdrasil.Domain.Entities;

public class FI_ConceptoFinanciado
{
    public int Id { get; set; }
    public int CreditoId { get; set; }
    public int CargoId { get; set; }
    public int TipoMovimientoId { get; set; }
    public decimal Monto { get; set; } = 0;
    public decimal Iva { get; set; } = 0;
    public decimal Total { get; set; } = 0;

    public FI_Credito FI_Credito { get; set; } = null!;
    public FI_Cargo FI_Cargo { get; set; } = null!;
    public FI_TipoMovimiento FI_TipoMovimiento { get; set; } = null!;
}
