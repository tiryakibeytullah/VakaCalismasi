using Nexum_TaskCase.Entities;

namespace Nexum_TaskCase.Repositories
{
    public interface IRotateAppService
    {
        public Direction Left(Direction direction);
        public Direction Right(Direction direction);
        public RoboticTraveler Move(RoboticTraveler roboticTraveler);
    }
}
