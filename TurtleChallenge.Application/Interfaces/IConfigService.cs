using System.Collections.Generic;
using TurtleChallenge.Model;
using TurtleChallenge.Model.Enum;

namespace TurtleChallenge.Application.Interfaces
{
    public interface IConfigService
    {
        Position Size { get; set; }
        Position StartPosition { get; set; }
        Position ExitPosition { get; set; }
        Direction StartDirection { get; set; }
        List<Position> Mines { get; set; }
        List<Movement> Movements { get; set; }

        void Configure(string configurationFile, string movementsFile);
    }
}
