using Yggdrasil.Common.Models.StoredProcedures;

namespace Yggdrasil.Module.Credito.Features.Configuracion.CalendarioLaboral.Commands;

public class CreateCalendarioLaboralCommand : ICommand<Result>
{
    public int? Anio { get; set; }
}

internal class CreateCalendarioLaboralCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<CreateCalendarioLaboralCommand, Result>
{
    public async Task<Result> HandleAsync(CreateCalendarioLaboralCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var returnValue = new OutputParameter<int>();
            await context.Procedures.usp_CreateCalendarioLaboralAsync(request.Anio, returnValue, cancellationToken);

            return Result.SuccessWithMessage("Calendario laboral generado correctamente.");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
