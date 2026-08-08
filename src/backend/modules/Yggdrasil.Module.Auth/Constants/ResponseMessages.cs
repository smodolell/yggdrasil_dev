namespace Yggdrasil.Module.Auth.Constants;

/// <summary>
/// Define los mensajes de respuesta estándar para las operaciones del módulo de seguridad.
/// </summary>
public static class ResponseMessages
{
    // Mensajes de éxito
    public const string UserCreatedSuccessfully = "El usuario fue creado exitosamente.";
    public const string UserUpdatedSuccessfully = "Los datos del usuario se almacenaron correctamente.";
    public const string RoleCreatedSuccessfully = "El rol fue creado exitosamente.";
    public const string RoleUpdatedSuccessfully = "El rol se actualizó correctamente.";
    public const string RoleDeletedSuccessfully = "El rol fue eliminado exitosamente.";
    public const string OperationSuccessful = "La operación se realizó con éxito.";
    public const string RolesSavedSuccessfully = "Los roles del usuario se guardaron correctamente.";


    // Mensajes de error
    public const string UserNotFound = "Usuario no encontrado.";
    public const string RoleNotFound = "Rol no encontrado.";
    public const string NotImplemented = "Funcionalidad no implementada.";
}
