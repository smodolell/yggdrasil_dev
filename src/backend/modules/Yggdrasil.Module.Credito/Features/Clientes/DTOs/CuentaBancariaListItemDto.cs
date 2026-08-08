namespace Yggdrasil.Module.Credito.Features.Clientes.DTOs;

public class CuentaBancariaListItemDto
{

    public int Id { get; set; }

    public string NomBanco { get; set; } = "";
    public string NomMoneda { get; set; } = "";
    public string NomTipoCuentaBancaria { get; set; } = "";
    public string NroCuentaBancaria { get; set; } = "";
    public string CBU { get; set; } = "";
    public string AliasCBU { get; set; } = "";
}
