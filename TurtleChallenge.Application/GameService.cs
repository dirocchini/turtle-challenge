using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TurtleChallenge.Application.Interfaces;
using TurtleChallenge.Model.Enum;

namespace TurtleChallenge.Application
{
    public class GameService : IGameService
    {
        private readonly IConfigService _configService;
        private readonly ITurtleService _turtleService;
        private readonly IAnalysisService _analysisService;
        private readonly ILogger<GameService> _logger;

        public GameService(IConfigService configService, ITurtleService turtleService, IAnalysisService analysisService, ILoggerFactory logger)
        {
            _configService = configService;
            _turtleService = turtleService;
            _analysisService = analysisService;
            _logger = logger.CreateLogger<GameService>();
        }

        public void Start(string gameSettingsFile, string movesFile)
        {
            try
            {
                _configService.Configure(gameSettingsFile, movesFile);
                _turtleService.Position = _configService.StartPosition;
                _turtleService.Direction = _configService.StartDirection;

                int iteration = 0;

                foreach (var movement in _configService.Movements.ToList().Select((value, i) => new { i, value }))
                {

                    iteration = movement.i;

                    if (movement.value == Movement.Move)
                        _turtleService.Move();

                    else if (movement.value == Movement.Rotate)
                        _turtleService.Rotate();

                    var status = _analysisService.Analyse(_turtleService.Position, _configService.Mines, _configService.Size, _configService.ExitPosition);

                    if (status == Status.ExitFound)
                    {
                        _logger.LogDebug($"Sequence {iteration}: Turtle Has Found The Exit!");
                        break;
                    }

                    else if (status == Status.IsDead)
                    {
                        _logger.LogDebug($"Sequence {iteration}: Sorry! You're Dead.");
                        break;
                    }

                    else if (status == Status.IsOutOfBoundaries)
                    {
                        _logger.LogDebug($"Sequence {iteration}: Sorry! You're Out of Boundaries");
                        break;
                    }

                    else
                        _logger.LogDebug($"Sequence {iteration}: Sucess!");
                }
                iteration++;
                if (_analysisService.Analyse(_turtleService.Position, _configService.Mines, _configService.Size, _configService.ExitPosition) == Status.IsAlive)
                    _logger.LogDebug($"Sequence {iteration}: Still in Danger!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Sorry, an error occuried. Details: {ex.ToString()}");
            }
        }
    }
}
