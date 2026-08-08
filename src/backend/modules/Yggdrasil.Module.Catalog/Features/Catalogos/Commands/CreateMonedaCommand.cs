using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class CreateMonedaCommand : ICommand<Result<int>>
{
    public required MonedaEditDto Model { get; set; }
}

public class CreateMonedaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<MonedaEditDto> validator,
    IConsecutivoService consecutivoService,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateMonedaCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<MonedaEditDto> _validator = validator;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<int>> HandleAsync(CreateMonedaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var consecutivoResult = await _consecutivoService.ObtenerSiguienteConsecutivoAsync(nameof(CAT_Moneda), cancellationToken);
            if (!consecutivoResult.Success)
            {
                Exception exception = new(consecutivoResult.ErrorMessage);
                throw exception;
            }


            var oMoneda = new CAT_Moneda
            {
                Id = consecutivoResult.ConsecutivoGenerado
            };
            _context.CAT_Moneda.Add(oMoneda);
            _mapper.Map(model, oMoneda);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Result.Success(oMoneda.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error(ex.Message);
        }

    }
}
