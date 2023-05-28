using Nexum_TaskCase.Entities;
using Nexum_TaskCase.Repositories;

namespace Nexum_TaskCase.Services
{
    public class RotateAppService : IRotateAppService
    {
        public Direction Left(Direction direction)
        {
            switch (direction.Position)
            {
                case Consts.Compass.North:
                    direction.Position = Consts.Compass.West;
                    break;
                case Consts.Compass.West:
                    direction.Position = Consts.Compass.South;
                    break;
                case Consts.Compass.South:
                    direction.Position = Consts.Compass.East;
                    break;
                case Consts.Compass.East:
                    direction.Position = Consts.Compass.North;
                    break;
            }

            return direction;
        }

        public Direction Right(Direction direction)
        {
            switch (direction.Position)
            {
                case Consts.Compass.North:
                    direction.Position = Consts.Compass.East;
                    break;
                case Consts.Compass.East:
                    direction.Position = Consts.Compass.South;
                    break;
                case Consts.Compass.South:
                    direction.Position = Consts.Compass.West;
                    break;
                case Consts.Compass.West:
                    direction.Position = Consts.Compass.North;
                    break;
            }

            return direction;
        }

        public RoboticTraveler Move(RoboticTraveler roboticTraveler)
        {
            switch (roboticTraveler.Direction.Position)
            {
                case Consts.Compass.North:
                    roboticTraveler.Coordinate.Y += 1;
                    break;
                case Consts.Compass.West:
                    roboticTraveler.Coordinate.X -= 1;
                    break;
                case Consts.Compass.South:
                    roboticTraveler.Coordinate.Y -= 1;
                    break;
                case Consts.Compass.East:
                    roboticTraveler.Coordinate.X += 1;
                    break;
            }

            return roboticTraveler;
        }
    }
}
