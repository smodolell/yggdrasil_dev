using LiteBus.Events;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Tharga.Console;
using Tharga.Console.Commands;
using Tharga.Console.Consoles;
using Tharga.Console.Entities;
using Tharga.Console.Interfaces;
using Yggdrasil.Tools.Commands;
using Yggdrasil.Tools.Templates.Base;

internal class Program
{
    private static void Main(string[] args)
    {
        IConsole? console = null;
        try
        {

            using (console = new ClientConsole(new ConsoleConfiguration()))
            {


                var services = new ServiceCollection();


                services.Scan(scan => scan
                .FromAssemblies(typeof(Program).Assembly)
                .AddClasses(classes => classes.AssignableTo<ICommand>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());


                services.AddTransient<Build_ContainerCommand>();
                services.AddTransient<Build_ModulesCommand>();

                // 1. Configurar Logging
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Debug);
                });

                var appLogger = loggerFactory.CreateLogger<Program>();
                var configCommandLogger = loggerFactory.CreateLogger<Build_ContainerCommand>();
                services.AddSingleton(configCommandLogger);

                services.AddLogging(configure => configure.AddConsole());
                services.AddSingleton<IMapper>(serviceProvider =>
                {
                    var config = TypeAdapterConfig.GlobalSettings;
                    config.Scan(Assembly.GetExecutingAssembly());
                    return new Mapper(config);
                });
                //services.AddLiteBus(configuration =>
                //{
                //    configuration.AddEventModule(m => m.RegisterFromAssembly(typeof(Program).Assembly)); // registra handlers
                //});

                TemplateConfig.Configure();


                var serviceProvider = services.BuildServiceProvider();

                var command = new RootCommand(console, new CommandResolver(type => (ICommand)serviceProvider.GetRequiredService(type)));

                command.RegisterCommand<Build_ContainerCommand>();

                var commandEngine = new CommandEngine(command)
                {
                    TaskRunners = new[]
                        {
                            new TaskRunner(async (c, a) =>
                            {
                                await Task.Delay(1000, c);
                            })
                        }
                };

                commandEngine.Start(args);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Fatal Error.");
            console?.OutputError(exception);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        finally
        {
            console?.Dispose();
        }
    }
}