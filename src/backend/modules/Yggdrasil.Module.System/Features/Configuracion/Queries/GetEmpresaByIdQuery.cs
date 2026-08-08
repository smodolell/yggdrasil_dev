using Yggdrasil.Module.System.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.System.Features.Configuracion.Queries;

public class GetEmpresaByIdQuery : IQuery<Result<EmpresaDto>>
{
    public int EmpresaId { get; set; }
}
public class GetEmpresaByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetEmpresaByIdQuery, Result<EmpresaDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<EmpresaDto>> HandleAsync(GetEmpresaByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oEmpresa = await _context.CAT_Empresa.SingleOrDefaultAsync(r => r.Id == message.EmpresaId, cancellationToken);
        if (oEmpresa == null)
        {
            return Result.NotFound();
        }
        var empresaDto = _mapper.Map<EmpresaDto>(oEmpresa);
        return Result.Success(empresaDto);
    }
}

