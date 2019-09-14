using Microsoft.Extensions.DependencyInjection;
using System;
using TurtleChallenge.Application;
using TurtleChallenge.Application.Interfaces;


namespace TurtleChallenge.CrossCutting.DI
{
    public static class DIModule
    {
        public static void ConfigureServices<TEntity>(IServiceCollection serviceCollection)
        {
            try
            {
                serviceCollection.AddScoped<IAnalysisService, AnalysisService>();
                serviceCollection.AddScoped<IConfigService, ConfigService>();
                serviceCollection.AddScoped<IGameService, GameService>();
                serviceCollection.AddScoped<ITurtleService, TurtleService>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error ocurried on DIModule. Details: {ex.ToString()}");
            }
        }
    }
}