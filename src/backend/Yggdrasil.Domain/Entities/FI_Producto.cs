namespace Yggdrasil.Domain.Entities;

public class FI_Producto
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }

    public int MonedaId { get; set; }

    [Required]
    [MaxLength(5)]
    public string ClaveProducto { get; set; } = "";

    [Required]
    [MaxLength(200)]
    public string NomProducto { get; set; } = "";

    [Required]
    [MaxLength(8)]
    public string Posfijo { get; set; } = "";

    [Required]
    [MaxLength(8)]
    public string Prefijo { get; set; } = "";


    [Required]
    [ConcurrencyCheck]
    public int Consecutivo { get; set; }

    [Required]
    public int? TipoMovimientoRentaId { get; set; }

    [Required]
    public int? TipoMovimientoMoraId { get; set; }



    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal TasaMoraDefault { get; set; }

    public int MoraPeriodoGracia { get; set; }

    [Column(TypeName = "decimal(8, 4)")]
    public decimal FactorTasaMora { get; set; }







    public bool Activo { get; set; } = true;

    [ForeignKey(nameof(MonedaId))]
    public CAT_Moneda CAT_Moneda { get; set; } = null!;

    [ForeignKey(nameof(EmpresaId))]
    public CAT_Empresa CAT_Empresa { get; set; } = null!;


    public ICollection<FI_Cargo> FI_Cargo { get; set; } = new HashSet<FI_Cargo>();


    //[ForeignKey(nameof(EsquemaFinancieroId))]
    //public CAT_EsquemaFinanciero CAT_EsquemaFinanciero { get; set; } = null!;
    //public ICollection<FI_ProductoFase> FI_ProductoFase { get; set; } = new HashSet<FI_ProductoFase>();
    //public ICollection<OT_Plan> OTA_Plan { get; set; } = new HashSet<OTA_Plan>();

}
