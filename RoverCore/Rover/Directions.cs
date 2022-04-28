namespace RoverCore.Rover;

public static class Directions
{
    public const char North = 'N';
    public const char East = 'E';
    public const char South = 'S';
    public const char West = 'W';

    public static char Turn(char currentDirection, char rotation)
    {
        return rotation switch
        {
            Commands.Right => TurnRight(currentDirection),
            Commands.Left => TurnLeft(currentDirection),
            _ => throw new IndexOutOfRangeException("Unrecognized rover rotation command.")
        };
    }
    
    private static char TurnLeft(char currentDirection) =>
        currentDirection switch
        {
            North => West,
            East => North,
            South => East,
            West => South,
            _ => throw new IndexOutOfRangeException("Unrecognized rover direction when turning left.")
        };

    private static char TurnRight(char currentDirection) =>
        currentDirection switch
        {
            North => East,
            East => South,
            South => West,
            West => North,
            _ => throw new IndexOutOfRangeException("Unrecognized rover direction when turning left.")
        };
}