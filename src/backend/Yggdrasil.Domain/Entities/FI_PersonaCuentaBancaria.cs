namespace Yggdrasil.Domain.Entities;

public class FI_PersonaCuentaBancaria
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public int BancoId { get; set; }

    public int MonedaId { get; set; }


    [Required]
    [MaxLength(50)]
    public string NroCuentaBancaria { get; set; } = "";

    [MaxLength(30)]
    public string CBU { get; set; } = "";

    [MaxLength(100)]
    public string AliasCBU { get; set; } = "";




    [ForeignKey(nameof(BancoId))]
    public CAT_Banco CAT_Banco { get; set; } = null!;


    [ForeignKey(nameof(MonedaId))]
    public CAT_Moneda CAT_Moneda { get; set; } = null!;




    [ForeignKey(nameof(PersonaId))]
    public FI_Persona? FI_Persona { get; set; }


}
