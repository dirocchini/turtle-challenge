using TurtleChallenge.Application.Interfaces;
using TurtleChallenge.Model;
using TurtleChallenge.Model.Enum;

namespace TurtleChallenge.Application
{
    public class TurtleService : ITurtleService
    {
        public TurtleService()
        {
        }

        public Position Position { get; set; }
        public Direction Direction { get; set; }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.North:
                    Position = new Position(Position.X, Position.Y - 1);
                    break;

                case Direction.East:
                    Position = new Position(Position.X + 1, Position.Y);
                    break;

                case Direction.South:
                    Position = new Position(Position.X, Position.Y + 1);
                    break;

                case Direction.West:
                    Position = new Position(Position.X - 1, Position.Y);
                    break;
            }
        }

        public void Rotate()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.East;
                    break;

                case Direction.East:
                    Direction = Direction.South;
                    break;

                case Direction.South:
                    Direction = Direction.West;
                    break;

                case Direction.West:
                    Direction = Direction.North;
                    break;
            }
        }
    }
}
