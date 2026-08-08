namespace Yggdrasil.Domain.Entities;
public class CAT_TasaValor
{
    public int Id { get; set; }
    public int TasaId { get; set; }

    public decimal ValorTasa { get; set; }

    public DateTime? Fecha { get; set; }

    public DateTime FechaRegistro{ get; set; }

    public CAT_Tasa CAT_Tasa { get; set; } = null!;
}
