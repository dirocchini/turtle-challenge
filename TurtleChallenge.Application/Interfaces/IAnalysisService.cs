using System.Collections.Generic;
using TurtleChallenge.Model;
using TurtleChallenge.Model.Enum;

namespace TurtleChallenge.Application.Interfaces
{
    public interface IAnalysisService
    {
        Status Analyse(Position turtlePosition, List<Position> mines, Position size, Position exit);
    }
}
