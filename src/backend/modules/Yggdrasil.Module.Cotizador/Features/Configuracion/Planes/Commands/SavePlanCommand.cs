using Humanizer;
using System;
using System.Collections.Generic;
using System.Text;
using Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

namespace Yggdrasil.Module.Cotizador.Features.Configuracion.Planes.Commands;

public record SavePlanCommand(int PlanId, PlanEditDto Model) : ICommand<Result<int>>;

internal class SavePlanCommandHandler(
    IApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IMapper mapper,IValidator<PlanEditDto> validator
) : ICommandHandler<SavePlanCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<PlanEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(SavePlanCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }


        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var plan = await _context.OT_Plan.SingleOrDefaultAsync(r => r.Id == message.PlanId, cancellationToken);
            if (plan == null && message.PlanId != 0)
                return Result.NotFound("Plan not found");

            var isNew = plan == null;
            if (isNew)
            {
                plan = new OT_Plan { EdadMinima = 18, EdadMaxima = 90, ImporteMinimo = 1 };
                await _context.OT_Plan.AddAsync(plan, cancellationToken);
            }


            _mapper.Map(model, plan);

            foreach (var item in model.Periodicidades)
            {
                var planPeriodicidad = await _context.OT_PlanPeriodicidad.
                    SingleOrDefaultAsync(r => r.PeriodicidadId == item.PeriodicidadId && r.PlanId == plan!.Id, cancellationToken);

                if (planPeriodicidad != null && item.Activo) continue;
                if (planPeriodicidad == null && !item.Activo) continue;
                if (planPeriodicidad == null && item.Activo)
                {
                    planPeriodicidad = new OT_PlanPeriodicidad
                    {
                        PeriodicidadId = item.PeriodicidadId
                    };
                    plan!.OT_PlanPeriodicidad.Add(planPeriodicidad);
                }
                else if (planPeriodicidad != null)
                {
                    _context.OT_PlanPeriodicidad.Remove(planPeriodicidad);
                }
            }






            await _unitOfWork.CommitTransactionAsync(cancellationToken);




            return Result.Success(plan!.Id);

        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error(ex.Message);
        }





    }
}