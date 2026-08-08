namespace Yggdrasil.Domain.Entities;
public class FI_Cargo
{
    public int Id { get; set; }

    public int ProductoId { get; set; }
    public int TipoMovimientoId { get; set; }
    public int TipoCalculoId { get; set; }
    public int? FormaPagoId { get; set; }


    [Required]
    [MaxLength(80)]
    public string Concepto { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal Monto { get; set; }

    [Required]
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Porcentaje { get; set; }

    [Required]
    public int EquivaleNroPeriodos { get; set; } = 0;

    public bool EsCargoInicial { get; set; }
    public bool EsConceptoFinanciado { get; set; }


    public bool PermiteEdicion { get; set; }
    public bool Activo { get; set; }



    [ForeignKey(nameof(ProductoId))]
    public FI_Producto FI_Producto { get; set; } = null!;

    [ForeignKey(nameof(TipoMovimientoId))]
    public FI_TipoMovimiento FI_TipoMovimiento { get; set; } = null!;

    [ForeignKey(nameof(TipoCalculoId))]
    public FI_TipoCalculo FI_TipoCalculo { get; set; } = null!;

    [ForeignKey(nameof(FormaPagoId))]
    public FI_FormaPago? FI_FormaPago { get; set; }


}
