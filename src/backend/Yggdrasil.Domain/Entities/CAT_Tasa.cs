namespace Yggdrasil.Domain.Entities;

public class CAT_Tasa
{
    public int Id { get; set; }
    public decimal ValorTasa { get; set; }
    public string NomTasa { get; set; } = "";
    public bool EsVariable { get; set; } = false;
    public bool Activo { get; set; }
    public ICollection<CAT_TasaValor> CAT_TasaValor { get; set; } = new HashSet<CAT_TasaValor>();
}
