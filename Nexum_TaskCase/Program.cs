using Nexum_TaskCase.Entities;
using Nexum_TaskCase.Services;
using static Nexum_TaskCase.Consts;

class Program
{

    static void Main(string[] args)
    {
        int maxX, maxY;

        Console.Write($"Mars yüzeyinin sağ üst noktasının koordinatları: ");

        string[] rTopCoordinate = Console.ReadLine().Split();
        bool resultMaxX = int.TryParse(rTopCoordinate[0], out maxX);
        bool resultMaxY = int.TryParse(rTopCoordinate[1], out maxY);

        if (resultMaxX && resultMaxY)
        {
            Console.Write($"İlk robotik gezginin başlangıç konumunu ve yönü: ");
            string[] positionOfFirstRobotic = Console.ReadLine().Split();
            RoboticTraveler firstRoboticTraveler = ValidateRoboticTraveler(positionOfFirstRobotic);

            Console.Write("İlk robotik gezginin hareket komutlarını giriniz: ");
            string firstRoboticCommands = Console.ReadLine();
            ValidateMovementCommands(firstRoboticCommands.ToCharArray());

            Console.Write($"İkinci robotik gezginin başlangıç konumunu ve yönü: ");
            string[] positionOfSecondRobotic = Console.ReadLine().Split();
            RoboticTraveler secondRoboticTraveler = ValidateRoboticTraveler(positionOfSecondRobotic);

            Console.Write("İkinci robotik gezginin hareket komutlarını giriniz: ");
            string secondRoboticCommands = Console.ReadLine();
            ValidateMovementCommands(secondRoboticCommands.ToCharArray());

            firstRoboticTraveler = ExecuteRoboticTraveler(firstRoboticCommands, firstRoboticTraveler);
            secondRoboticTraveler = ExecuteRoboticTraveler(secondRoboticCommands, secondRoboticTraveler);

            firstRoboticTraveler.Coordinate.X = Math.Min(Math.Max(firstRoboticTraveler.Coordinate.X, 0), maxX);
            firstRoboticTraveler.Coordinate.Y = Math.Min(Math.Max(firstRoboticTraveler.Coordinate.Y, 0), maxY);

            secondRoboticTraveler.Coordinate.X = Math.Min(Math.Max(secondRoboticTraveler.Coordinate.X, 0), maxX);
            secondRoboticTraveler.Coordinate.Y = Math.Min(Math.Max(secondRoboticTraveler.Coordinate.Y, 0), maxY);

            Console.WriteLine();
            Console.WriteLine($"İlk robotik gezginin son konumu: {firstRoboticTraveler.Coordinate.X} {firstRoboticTraveler.Coordinate.Y} {firstRoboticTraveler.Direction.Position}");

            Console.WriteLine($"İkinci robotik gezginin son konumu: {secondRoboticTraveler.Coordinate.X} {secondRoboticTraveler.Coordinate.Y} {secondRoboticTraveler.Direction.Position}");

        }
        else
        {
            Console.WriteLine("Lütfen sağ üst noktasının koordinat değerleri için sadece sayı girişi yapınız !");
        }
    }

    public static RoboticTraveler ValidateRoboticTraveler(string[] position)
    {
        var directions = new List<string>() { Compass.North, Compass.East, Compass.South, Compass.West };
        int xCoordinate, yCoordinate;

        bool resultXCoordinate = int.TryParse(position[0], out xCoordinate);
        bool resultYCoordinate = int.TryParse(position[1], out yCoordinate);
        if (resultXCoordinate && resultYCoordinate && directions.Contains(position[2].ToUpper()))
        {
            return new RoboticTraveler
            {
                Coordinate = new Coordinate(xCoordinate, yCoordinate),
                Direction = new Direction(position[2].ToUpper())
            };
        }
        else
        {
            Console.WriteLine($"Girmiş olduğunuz konum ve yön bilgileri yanlış !");
            Environment.Exit(0);
            return null;
        }
    }

    public static void ValidateMovementCommands(char[] array)
    {
        var rotations = new List<string>() { Rotation.Left, Rotation.Right, Rotation.Move };
        foreach (var item in array)
        {
            if (!rotations.Contains(item.ToString().ToUpper()))
            {
                Console.WriteLine($"Girmiş olduğunuz gezginin hareket komutları yanlış !");
                Environment.Exit(0);
            }
        }
    }

    public static RoboticTraveler ExecuteRoboticTraveler(string commands, RoboticTraveler roboticTraveler)
    {
        RotateAppService rotateAppService = new RotateAppService();
        foreach (char c in commands)
        {
            switch (c.ToString().ToUpper())
            {
                case Rotation.Left:
                    rotateAppService.Left(roboticTraveler.Direction); ;
                    break;
                case Rotation.Right:
                    rotateAppService.Right(roboticTraveler.Direction);
                    break;
                case Rotation.Move:
                    rotateAppService.Move(roboticTraveler);
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        return roboticTraveler;
    }
}