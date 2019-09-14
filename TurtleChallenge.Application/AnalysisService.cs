using System.Collections.Generic;
using TurtleChallenge.Application.Interfaces;
using TurtleChallenge.Model;
using TurtleChallenge.Model.Enum;

namespace TurtleChallenge.Application
{
    public class AnalysisService : IAnalysisService
    {
        public Status Analyse(Position turtlePosition, List<Position> mines, Position size, Position exit)
        {
            if (IsOutOfBoundaries(size, turtlePosition))
                return Status.IsOutOfBoundaries;

            else if (IsDead(mines, turtlePosition))
                return Status.IsDead;

            else if (ExitFound(exit, turtlePosition))
                return Status.ExitFound;
            
            return Status.IsAlive;
        }

        private bool ExitFound(Position exit, Position turtlePosition)
        {
            return turtlePosition.X == exit.X && turtlePosition.Y == exit.Y;
        }

        private bool IsDead(List<Position> mines, Position turtlePosition)
        {
            foreach (var mine in mines)
            {
                if (mine.X == turtlePosition.X && mine.Y == turtlePosition.Y)
                    return true;
            }

            return false;
        }

        private bool IsOutOfBoundaries(Position size, Position turtlePosition)
        {
            return turtlePosition.X < 0 || turtlePosition.X > (size.X -1) || turtlePosition.Y < 0 || turtlePosition.Y > (size.Y-1);
        }
    }
}
