using Microsoft.AspNetCore.Routing;

namespace Yggdrasil.Common.Endpoint;


public abstract class EndpointGroupBase
{
    public virtual string? GroupName { get; }
    public abstract void Map(RouteGroupBuilder groupBuilder);
}
