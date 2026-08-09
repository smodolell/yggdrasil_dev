using Yggdrasil.Module.Credito.Features.Creditos.DTOs;

namespace Yggdrasil.Module.Credito.Features.Creditos.Queries;

public record GetPagoDetailByIdQuery(int PagoId) : IQuery<Result<PagoDetailDto>>;


internal class GetPagoDetailByIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetPagoDetailByIdQuery, Result<PagoDetailDto>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<PagoDetailDto>> HandleAsync(GetPagoDetailByIdQuery message, CancellationToken cancellationToken = default)
    {
        var pagoId = message.PagoId;

        var result = await _context.FI_Pago
            .Include(i => i.FI_PagoMovimiento)
                .ThenInclude(tm => tm.FI_Movimiento)
            .Where(p => p.Id == pagoId)
            .Select(p => new PagoDetailDto
            {
                PagoId = p.Id,
                FechaRegistro = p.FechaRegistro,
                FechaPago = p.FechaPago,
                FechaModificacion = p.FechaModificacion,
                Monto = p.Monto,
                SaldoFavor = p.SaldoFavor,
                Detalles = p.FI_PagoMovimiento
                    .Where(pm => pm.FI_Movimiento != null)
                    .OrderBy(pm => pm.FI_Movimiento.FechaVencimiento)
                    .Select(pm => new PagoDetailItemDto
                    {
                        MovimientoId = pm.MovimientoId,
                        DescMovimiento = pm.FI_Movimiento.DescMovimiento,
                        FechaVencimiento = pm.FI_Movimiento.FechaVencimiento,
                        Capital = pm.FI_Movimiento.Capital,
                        Interes = pm.FI_Movimiento.Interes,
                        Iva = pm.FI_Movimiento.Iva,
                        Total = pm.FI_Movimiento.Total,
                        SaldoCapital = pm.FI_Movimiento.SaldoCapital,
                        SaldoInteres = pm.FI_Movimiento.SaldoInteres,
                        SaldoIva = pm.FI_Movimiento.SaldoIva,
                        SaldoTotal = pm.FI_Movimiento.SaldoTotal,
                        TotalPagado = pm.TotalPagado,
                        CapitalPagado = pm.CapitalPagado,
                        InteresPagado = pm.InteresPagado,
                        IvaPagado = pm.IvaPagado,
                        FechaPago = pm.FechaPago,
                        Cancelado = pm.Cancelado,
                        MotivoCancelacion = pm.MotivoCancelacion ?? ""
                    }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
            return Result.Invalid(new ValidationError("No se encontró el pago especificado."));

        return Result.Success(result);
    }
}