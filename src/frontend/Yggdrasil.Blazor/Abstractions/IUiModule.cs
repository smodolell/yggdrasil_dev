namespace Yggdrasil.Blazor.Abstractions;

public interface IUiModule
{
    Guid ModuleId { get; }
    string Name { get; }
    string Description { get; }
    string Icon { get; }
    // El Assembly es clave para que la reflexión sepa dónde buscar las páginas
    System.Reflection.Assembly ModuleAssembly { get; }
}
