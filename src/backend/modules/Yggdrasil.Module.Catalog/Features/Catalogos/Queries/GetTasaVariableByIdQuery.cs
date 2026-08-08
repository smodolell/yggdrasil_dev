using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public record GetTasaVariableByIdQuery(int Id) : IQuery<Result<TasaVariableDetalleDto>>;

internal class GetTasaVariableByIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetTasaVariableByIdQuery, Result<TasaVariableDetalleDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<TasaVariableDetalleDto>> HandleAsync(GetTasaVariableByIdQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.CAT_Tasa
                .Include(t => t.CAT_TasaValor)
                .SingleOrDefaultAsync(t => t.Id == request.Id && t.EsVariable, cancellationToken);

            if (entity == null)
            {
                return Result.NotFound("Tasa variable no encontrada");
            }

            var dto = new TasaVariableDetalleDto
            {
                Id = entity.Id,
                NomTasa = entity.NomTasa,
                Valores = entity.CAT_TasaValor
                    .OrderByDescending(v => v.Fecha)
                    .Select(v => new TasaValorListItemDto
                    {
                        Id = v.Id,
                        Valor = v.ValorTasa,
                        Fecha = v.Fecha,
                        FechaRegistro = v.FechaRegistro
                    })
                    .ToList()
            };

            return Result.Success(dto);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
