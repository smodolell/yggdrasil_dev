using Tharga.Console.Commands.Base;

namespace Yggdrasil.Tools.Commands;

internal class Build_ContainerCommand : ContainerCommandBase
{
    public Build_ContainerCommand() : base("build")
    {
        RegisterCommand<Build_ModulesCommand>();
    }
}
