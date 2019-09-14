using TurtleChallenge.Model;
using TurtleChallenge.Model.Enum;

namespace TurtleChallenge.Application.Interfaces
{
    public interface ITurtleService
    {
        Position Position { get; set; }
        Direction Direction { get; set; }
        void Move();
        void Rotate();
    }
}
