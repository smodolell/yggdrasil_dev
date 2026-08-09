namespace Yggdrasil.Module.Credito.Features.Operaciones.ActivarCredito.DTOs;

public class ActivarCreditoResultDto
{
    public bool HasError { get; set; }
    public string MessageProcess { get; set; } = "";
    public int CreditoId { get; set; }
    public string ClaveCredito { get; set; } = "";
}