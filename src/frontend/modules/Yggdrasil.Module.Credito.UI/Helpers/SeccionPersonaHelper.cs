using Microsoft.AspNetCore.Components;
using System.Reflection;
using Yggdrasil.Module.Credito.UI.Attributes;
using Yggdrasil.Module.Credito.UI.Interfaces;

namespace Yggdrasil.Module.Credito.UI.Helpers;

public static class SeccionPersonaHelper
{
    public static List<SeccionPersonaAssemblyDto> GetListSeccionByAssembly(Assembly assembly, List<int>? seccionesPermitidas = null)
    {
        var components = assembly.ExportedTypes
            .Where(t => t.IsSubclassOf(typeof(ComponentBase)));
        var result = new List<SeccionPersonaAssemblyDto>();

        foreach (var typeComponent in components)
        {
            var attributes = typeComponent.GetCustomAttributes(inherit: true);
            var seccionAttribute = attributes.OfType<SeccionPersonaAttribute>().FirstOrDefault();
            if (seccionAttribute is null) continue;

            var isCreate = ImplementsInterface(typeComponent, typeof(ISeccionPersonaCreate));
            var isEdit = ImplementsInterface(typeComponent, typeof(ISeccionPersonaEdit));
            var isExtension = ImplementsInterface(typeComponent, typeof(ISeccionPersonaExtension));
            if (seccionesPermitidas != null && !seccionesPermitidas.Contains(seccionAttribute.Id))
                continue;

            result.Add(new SeccionPersonaAssemblyDto
            {
                SeccionId = seccionAttribute.Id,
                NomSeccion = seccionAttribute.NomSeccion,
                TypeComponent = typeComponent,
                IsCreate = isCreate,
                IsEdit = isEdit,
                IsExtension = isExtension
            });
        }

        return result;
    }

    public static bool ImplementsInterface(Type type, Type interfaceType)
    {
        return type.GetInterfaces()
                  .Any(i => interfaceType.IsAssignableFrom(i));
    }

}

public class SeccionPersonaAssemblyDto
{
    public int SeccionId { get; set; }

    public string NomSeccion { get; set; } = "";

    public Type? TypeComponent { get; set; }

    public DynamicComponent? Ref { get; set; }

    public bool IsValid { get; set; }

    public bool IsCreate { get; set; }
    public bool IsEdit { get; set; }
    public bool IsExtension { get; set; }


}


