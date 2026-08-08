using Tharga.Console.Commands.Base;

namespace Yggdrasil.Tools.Commands;

internal class Build_ModulesCommand : ActionCommandBase
{

    public Build_ModulesCommand() : base("modules", "Crea un modulo")
    {
    }

    public override async void Invoke(string[] param)
    {

        
        //ParameterBase.Instance.Configure(config =>
        //{
        //    config.PathOutput = path;
        //    config.SolutionName = $"{model.Name}_output";
        //    config.NameSpaceBase = model.NameSpaceBase;
        //    config.ApplicationName = model.Name;
        //});

        //var appEvent = spec.BuildEvent();
        //appEvent.LogEvent();

        //await _eventPublisher.PublishAsync(spec.BuildEvent());
    }
}