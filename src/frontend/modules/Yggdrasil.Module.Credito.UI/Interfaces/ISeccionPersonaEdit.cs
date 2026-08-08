using Ardalis.Result;
using Microsoft.AspNetCore.Components;

namespace Yggdrasil.Module.Credito.UI.Interfaces;

public interface ISeccionPersonaEdit
{
    public int PersonaId { get; set; }
    Task<Result> Save();
    public EventCallback<(int, bool)> OnValidationChanged { get; set; }
    Task ValidateAsync();
}
