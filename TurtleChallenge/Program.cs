using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;
using TurtleChallenge.Application.Interfaces;
using TurtleChallenge.CrossCutting.DI;

namespace TurtleChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            DIModule.ConfigureServices<Program>(serviceCollection);

            var serviceProvider = serviceCollection.AddLogging(cfg => cfg.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug).BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Program>>();

            IGameService gameService = serviceProvider.GetService<IGameService>();

            gameService.Start(
                    (args.Length > 0 ? args[0] : null),
                    (args.Length > 1 ? args[1] : null));

            Thread.Sleep(500);
        }
    }
}
