using Yggdrasil.Module.Credito.Features.Configuracion.CalendarioLaboral.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.CalendarioLaboral.Commands;

public class UpdateCalendarioLaboralCommand : ICommand<Result>
{
    public int Id { get; set; }
    public required CalendarioLaboralEditDto Model { get; set; }
}

internal class UpdateCalendarioLaboralCommandHandler(
    IApplicationDbContext context,
    IValidator<CalendarioLaboralEditDto> validator
) : ICommandHandler<UpdateCalendarioLaboralCommand, Result>
{
    public async Task<Result> HandleAsync(UpdateCalendarioLaboralCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request.Model, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Invalid(validationResult.AsErrors());

            var entity = await context.CAT_CalendarioLaboral
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return Result.NotFound("Día del calendario laboral no encontrado");

            entity.EsHabil = request.Model.EsHabil;
            entity.Descripcion = request.Model.Descripcion;

            context.CAT_CalendarioLaboral.Update(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
