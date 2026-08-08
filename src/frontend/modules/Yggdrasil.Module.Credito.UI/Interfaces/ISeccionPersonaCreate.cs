using Ardalis.Result;
using Microsoft.AspNetCore.Components;

namespace Yggdrasil.Module.Credito.UI.Interfaces;

public interface ISeccionPersonaCreate
{
    Task<Result> Create(int personaId);
    public EventCallback<(int, bool)> OnValidationChanged { get; set; }

    Task ValidateAsync();
}
