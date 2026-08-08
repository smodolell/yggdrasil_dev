using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Yggdrasil.Common.Interfaces;

public interface IModule
{
    public IServiceCollection Add(IServiceCollection services) => services;

    public WebApplication Use(WebApplication app) => app;

    Guid GetModuleId();
    string GetModuleName();
    string GetModuleDescription();
}
