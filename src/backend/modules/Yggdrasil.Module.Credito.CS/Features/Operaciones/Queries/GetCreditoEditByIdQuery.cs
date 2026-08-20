using Yggdrasil.Module.Credito.CS.Features.Operaciones.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Operaciones.Queries;

public record GetCreditoEditByIdQuery(int Id) : IQuery<Result<CreditoCSEditDto>>;

internal class GetCreditoEditByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetCreditoEditByIdQuery, Result<CreditoCSEditDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<CreditoCSEditDto>> HandleAsync(GetCreditoEditByIdQuery message, CancellationToken cancellationToken = default)
    {
        var credito = await _context.CS_Credito
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Id == message.Id, cancellationToken);

        if (credito == null)
        {
            return Result.NotFound("Crédito no encontrado");
        }

        var dto = new CreditoCSEditDto
        {
            CreditoId = credito.Id,
            TipoCreditoId = credito.TipoCreditoId,
            PeriodicidadId = credito.PeriodicidadId,
            EstatusCreditoId = credito.EstatusCreditoId,
            MetodoArmotizacionId = credito.MetodoArmotizacionId,
            FechaInicio = credito.FechaInicio,
            FechaPrimeraRenta = credito.FechaPrimeraRenta,
            FechaFirmaContrato = credito.FechaFirmaContrato,
            FechaActivacion = credito.FechaActivacion,
            ClaveCredito = credito.ClaveCredito,
            Capital = credito.Capital,
            Tasa = credito.Tasa,
            TasaIva = credito.TasaIva,
            Plazo = credito.Plazo,
            VersionTabla = credito.VersionTabla
        };

        return Result.Success(dto);
    }
}
