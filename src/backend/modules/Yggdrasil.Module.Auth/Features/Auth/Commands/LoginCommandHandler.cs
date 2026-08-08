using Microsoft.AspNetCore.Identity;

namespace Yggdrasil.Module.Auth.Features.Auth.Commands;

public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<UsuarioLoginDto>>
{
    private readonly UserManager<SYS_Usuario> _userManager;
    private readonly SignInManager<SYS_Usuario> _signInManager;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        UserManager<SYS_Usuario> userManager,
        SignInManager<SYS_Usuario> signInManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<Result<UsuarioLoginDto>> HandleAsync(LoginCommand request, CancellationToken cancellationToken = default)
    {
        // 1. Buscar el usuario por Email o Username
        var usuario = await _userManager.FindByEmailAsync(request.Email ?? "")
                      ?? await _userManager.FindByNameAsync(request.Usuario ?? "");

        if (usuario == null)
        {
            return Result.Unauthorized("Credenciales inválidas");
        }

        // 2. Verificar la contraseña usando SignInManager
        // El tercer parámetro 'false' es para lockoutOnFailure (bloqueo por intentos fallidos)
        var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Contrasenia, false);

        if (!resultado.Succeeded)
        {
            return Result.Unauthorized("Credenciales inválidas");
        }

        // 3. Obtener roles (opcional, pero recomendado para el DTO)
        var roles = await _userManager.GetRolesAsync(usuario);
        var rolPrincipal = roles.FirstOrDefault() ?? "Usuario";

        // 4. Mapear al DTO
        var loginResponse = new UsuarioLoginDto
        {
            Id = usuario.Id,
            NombreCompleto = usuario.NombreCompleto, // Asumiendo que esta propiedad existe en SYS_Usuario
            Email = usuario.Email,
            UsuarioNombre = usuario.UserName,
            Role = rolPrincipal,
            RefreshToken = _jwtService.GenerateRefreshToken(),
            TokenExpiration = DateTime.UtcNow.AddMinutes(60)
        };

        // 5. Generar JWT
        loginResponse.Token = _jwtService.GenerateToken(loginResponse);

        return Result.Success(loginResponse, "Login exitoso");
    }
}