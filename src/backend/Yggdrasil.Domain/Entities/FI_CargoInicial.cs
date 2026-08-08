using Yggdrasil.Domain.Entities;

public class FI_CargoInicial
{
    public int Id { get; set; }
    public int CreditoId { get; set; }
    public int CargoId { get; set; }
    public int TipoMovimientoId { get; set; }

    [Column(TypeName = "decimal(13,2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "decimal(13,2)")]
    public decimal Iva { get; set; }

    [Column(TypeName = "decimal(13,2)")]
    public decimal Total { get; set; }


    [ForeignKey(nameof(CreditoId))]
    public FI_Credito FI_Credito { get; set; } = null!;


    [ForeignKey(nameof(CargoId))]
    public FI_Cargo FI_Cargo { get; set; } = null!;

    [ForeignKey(nameof(TipoMovimientoId))]
    public FI_TipoMovimiento FI_TipoMovimiento { get; set; } = null!;
}