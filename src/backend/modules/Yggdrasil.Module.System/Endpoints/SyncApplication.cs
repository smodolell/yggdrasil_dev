using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.System.Features.Sync.Commands;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.System.Endpoints;

public class SyncApplication : EndpointGroupBase
{
    public override string? GroupName => "sync-application";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Sistema - Sistema");

        group.MapPost("module", SyncModule)
          .WithName("SyncModule")
          .WithSummary("Sincroniza los módulos y puntos de acceso detectados por reflexión en el cliente")
          .Accepts<List<ModuleDto>>("application/json")
          .Produces<Result>(StatusCodes.Status200OK)
          .Produces<ApiResponseDto<object>>(StatusCodes.Status400BadRequest)
          .Produces<ApiResponseDto<object>>(StatusCodes.Status401Unauthorized)
          .Produces<ApiResponseDto<object>>(StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> SyncModule(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] List<ModuleDto> model)
    {
        var command = new SyncAccessPointCommand(model);
        var result = await commandMediator.SendAsync(command);
        return result.ToCustomMinimalApiResult();
    }
}