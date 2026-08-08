using Yggdrasil.Common.Constants;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public class CreatePersonaDefaultCommand : ICommand<Result<int>>
{
    public int PerfilId { get; set; }
    public DateTime? FechaRegistro { get; set; }
}
internal class CreatePersonaDefaultCommandHandler(IApplicationDbContext context) : ICommandHandler<CreatePersonaDefaultCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<int>> HandleAsync(CreatePersonaDefaultCommand message, CancellationToken cancellationToken = default)
    {
        var oPerfil = await _context.FI_Perfil.SingleOrDefaultAsync(r => r.Id == message.PerfilId);
        if (oPerfil == null) return Result.Invalid(new ValidationError("Perfil no encontrado"));

        var oPersona = new FI_Persona
        {
            PerfilId = oPerfil.Id,
            TipoPersonaId = AppConstants.CAT_TipoPersonaId_PersonaFisica,
            GeneroId = AppConstants.CAT_GeneroId_MASCULINO,
            PrimerNombre = "",
            SegundoNombre = "",
            ApellidoPaterno = "",
            ApellidoMaterno = "",
            EdoCivilId = 1,
            RFC = "",
            CURP = "",
            Email = "",
            FechaNacimiento = null,
            FechaRegistro = message.FechaRegistro ?? DateTime.Now,
            FechaAltaCliente = message.FechaRegistro ?? DateTime.Now,
            RazonSocial = "",
            FechaConstitucion = null,
            LugarNacimientoId = "NA",
            NSS = "",
            Identificador = "",
        };
        var oPersonaPerfil = new FI_PersonaPerfil
        {
            PerfilId = oPerfil.Id
        };

        oPersona.FI_PersonaPerfil.Add(oPersonaPerfil);
        _context.FI_Persona.Add(oPersona);


        try
        {
            await _context.SaveChangesAsync();

            return Result.Success(oPersona.Id);
        }

        catch (Exception ex)
        {

            return Result.Error(ex.Message);
        }

    }
}


