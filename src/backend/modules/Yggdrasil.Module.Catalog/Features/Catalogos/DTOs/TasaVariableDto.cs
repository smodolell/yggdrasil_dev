using System.ComponentModel.DataAnnotations;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

public class TasaVariableDto
{
    public string NomTasa { get; set; } = string.Empty;
    public bool Activo { get; set; }
}
