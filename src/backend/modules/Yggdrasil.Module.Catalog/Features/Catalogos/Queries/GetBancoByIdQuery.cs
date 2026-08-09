using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetBancoByIdQuery : IQuery<Result<BancoEditDto>>
{
    public int BancoId { get; set; }
}

public class GetBancoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetBancoByIdQuery, Result<BancoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<BancoEditDto>> HandleAsync(GetBancoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oBanco = await _context.CAT_Banco.SingleOrDefaultAsync(r => r.Id == message.BancoId, cancellationToken);
        if (oBanco == null)
        {
            return Result.NotFound();
        }
        var bancoDto = _mapper.Map<BancoEditDto>(oBanco);
        return Result.Success(bancoDto);
    }
}
