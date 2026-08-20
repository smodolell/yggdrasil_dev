namespace Yggdrasil.Module.Credito.CS.Features.Creditos.Specifications;

public class CreditoFilterSpecification : Specification<CS_Credito>
{
    public CreditoFilterSpecification(
        string? searchText = null,
        int? tipoCreditoId = null,
        int? estatusCreditoId = null,
        DateTime? fechaActivacionStart = null,
        DateTime? fechaActivacionEnd = null)
    {
        Query.Include(c => c.CS_TipoCredito)
             .Include(c => c.CS_EstatusCredito);

        ApplySearchTextFilter(searchText);
        ApplyEstatusFilter(estatusCreditoId);
        ApplyTipoCreditoFilter(tipoCreditoId);
        ApplyFechaActivacionFilter(fechaActivacionStart, fechaActivacionEnd);
    }

    private void ApplySearchTextFilter(string? searchText)
    {
        if (string.IsNullOrEmpty(searchText)) return;

        var terms = searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var term in terms)
        {
            var temp = term.Trim();
            if (string.IsNullOrEmpty(temp)) continue;

            Query.Where(c => c.ClaveCredito.Contains(temp));
        }
    }

    private void ApplyEstatusFilter(int? estatusCreditoId)
    {
        if (estatusCreditoId.HasValue && estatusCreditoId.Value != 0)
        {
            Query.Where(c => c.EstatusCreditoId == estatusCreditoId.Value);
        }
    }

    private void ApplyTipoCreditoFilter(int? tipoCreditoId)
    {
        if (tipoCreditoId.HasValue && tipoCreditoId.Value != 0)
        {
            Query.Where(c => c.TipoCreditoId == tipoCreditoId.Value);
        }
    }

    private void ApplyFechaActivacionFilter(DateTime? start, DateTime? end)
    {
        if (start.HasValue)
        {
            Query.Where(c => c.FechaActivacion >= start.Value);
        }

        if (end.HasValue)
        {
            Query.Where(c => c.FechaActivacion <= end.Value);
        }
    }
}
