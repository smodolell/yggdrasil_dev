using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class CreatePeriodicidadCommand : ICommand<Result<int>>
{
    public required PeriodicidadEditDto Model { get; set; }
}

public class CreatePeriodicidadCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<PeriodicidadEditDto> validator,
    IUnitOfWork unitOfWork,
    IConsecutivoService consecutivoService

) : ICommandHandler<CreatePeriodicidadCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<PeriodicidadEditDto> _validator = validator;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;

    public async Task<Result<int>> HandleAsync(CreatePeriodicidadCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var consecutivoResult = await _consecutivoService.ObtenerSiguienteConsecutivoAsync(nameof(CAT_Periodicidad), cancellationToken);
            if (!consecutivoResult.Success)
            {
                var exception = new Exception(consecutivoResult.ErrorMessage);
                throw exception;
            }

            var oPeriodicidad = new CAT_Periodicidad
            {
                Id = consecutivoResult.ConsecutivoGenerado
            };
            _context.CAT_Periodicidad.Add(oPeriodicidad);
            _mapper.Map(model, oPeriodicidad);


            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Result.Success(oPeriodicidad.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error(ex.Message);

        }



    }
}
