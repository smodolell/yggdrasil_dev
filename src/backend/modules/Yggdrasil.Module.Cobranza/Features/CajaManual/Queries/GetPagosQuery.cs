using Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.CajaManual.Queries;

public record GetPagosQuery(int? PersonaId, int? CreditoId) : IQuery<Result<List<PagoListItemDto>>>;

internal class GetPagosQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetPagosQuery, Result<List<PagoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<List<PagoListItemDto>>> HandleAsync(GetPagosQuery message, CancellationToken cancellationToken = default)
    {
        // Query base con joins
        var query = from p in _context.FI_Pago
                    join tp in _context.FI_TipoPago on p.TipoPagoId equals tp.Id
                    join pm in _context.FI_PagoMovimiento on p.Id equals pm.PagoId
                    join m in _context.FI_Movimiento on pm.MovimientoId equals m.Id
                    join c in _context.FI_Credito on m.CreditoId equals c.Id
                    select new { p, tp, pm, m, c };

        // Aplicar filtros dinámicamente
        if (message.PersonaId.HasValue && message.PersonaId.Value > 0)
        {
            query = query.Where(x => x.c.PersonaId == message.PersonaId.Value);
        }

        if (message.CreditoId.HasValue && message.CreditoId.Value > 0)
        {
            query = query.Where(x => x.c.Id == message.CreditoId.Value);
        }

        // Agrupar y proyectar el resultado
        var result = await query
            .GroupBy(x => new
            {
                x.p.Id,
                x.p.FechaRegistro,
                x.c.ClaveCredito,
                x.tp.NomTipoPago,
                x.p.FechaPago,
                x.p.Monto,
                x.p.SaldoFavor
            })
            .Select(g => new PagoListItemDto
            {
                PagoId = g.Key.Id,
                FechaRegistro = g.Key.FechaRegistro,
                ClaveCredito = g.Key.ClaveCredito,
                NomTipoPago = g.Key.NomTipoPago,
                FechaPago = g.Key.FechaPago,
                Monto = g.Key.Monto,
                MontoAplicado = g.Sum(x => x.pm.TotalPagado),
                SaldoFavor = g.Key.SaldoFavor
            })
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }
}
