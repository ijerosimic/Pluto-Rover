using Microsoft.Extensions.DependencyInjection;
using RoverCore.Rover;

namespace RoverCore
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var services = BuildServices(args);
            var app = services.GetRequiredService<App>();
            app.Run();
        }

        private static IServiceProvider BuildServices(string[] args) =>
            new ServiceCollection()
                .AddSingleton<App>()
                .AddSingleton(new Arguments(args))
                .AddSingleton<IOutputPrinter, OutputPrinter>()
                .AddSingleton<IRover, Rover.Rover>()
                .BuildServiceProvider();
    }
}