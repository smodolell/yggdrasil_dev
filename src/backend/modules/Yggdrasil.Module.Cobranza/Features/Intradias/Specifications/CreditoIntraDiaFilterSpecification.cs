namespace Yggdrasil.Module.Cobranza.Features.Intradias.Specifications;

public class CreditoIntraDiaFilterSpecification : Specification<DEV_CreditoIntraDia>
{
    public CreditoIntraDiaFilterSpecification(
        DateTime? fechaPrimeraRentaStart = null,
        DateTime? fechaPrimeraRentaEnd = null)
    {
        ApplyFechaPrimeraRentaFilter(fechaPrimeraRentaStart, fechaPrimeraRentaEnd);
    }

    private void ApplyFechaPrimeraRentaFilter(DateTime? start, DateTime? end)
    {
        if (start.HasValue)
        {
            Query.Where(c => c.FechaPrimeraRenta >= start.Value);
        }

        if (end.HasValue)
        {
            Query.Where(c => c.FechaPrimeraRenta <= end.Value);
        }
    }
}
