using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.System.Features.Layout.Queries;
using Yggdrasil.Module.System.Features.Layout.DTOs;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.System.Endpoints;

public class Layout : EndpointGroupBase
{
    public override string? GroupName => "layout";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Sistema - Layout");

        #region Navbar
        group.MapGet("navbar", GetNavbar)
            .WithName("GetNavbar")
            .WithSummary("Obtiene el menú de navegación (navbar) del usuario")
            .WithDescription("Retorna la estructura completa del menú lateral izquierdo con sus respectivos hijos")
            .Produces<ApiResponseDto<HashSet<AccessPointDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region Navbar
    public async Task<IResult> GetNavbar(
        [FromServices] IQueryMediator queryMediator)
    {
        var result = await queryMediator.QueryAsync(new GetNavbarQuery());
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}