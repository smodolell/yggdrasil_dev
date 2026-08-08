namespace Yggdrasil.Module.Credito.UI.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class SeccionPersonaAttribute : Attribute
{
    public int Id { get; set; }
    public string NomSeccion { get; set; } = "";

    public SeccionPersonaAttribute(int id, string nomSeccion)
    {
        Id = id;
        NomSeccion = nomSeccion;
    }

}
