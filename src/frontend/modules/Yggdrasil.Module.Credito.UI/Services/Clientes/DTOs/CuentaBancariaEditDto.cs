namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class CuentaBancariaEditDto
{
    public int? CuentaBancariaId { get; set; }
    public int PersonaId { get; set; }
    public int? BancoId { get; set; }

    public int? MonedaId { get; set; }
    public int? TipoCuentaBancariaId { get; set; }

    public string NroCuentaBancaria { get; set; } = "";

    public string CBU { get; set; } = "";

    public string AliasCBU { get; set; } = "";

}
