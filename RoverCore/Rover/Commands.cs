namespace RoverCore.Rover;

public static class Commands
{
    public const char Forward = 'F';
    public const char Backward = 'B';
    public const char Right = 'R';
    public const char Left = 'L';

    private static (int, int) MoveNorth((int X, int Y) currentPosition)
    {
        var (x, y) = currentPosition;
        if (y > 99)
            throw new ArgumentException("Command would move rover out of bounds.");

        return new ValueTuple<int, int>(x, ++y);
    }

    private static (int, int) MoveSouth((int X, int Y) currentPosition)
    {
        var (x, y) = currentPosition;
        if (y < 1)
            throw new ArgumentException("Command would move rover out of bounds.");

        return new ValueTuple<int, int>(x, --y);
    }

    private static (int, int) MoveEast((int X, int Y) currentPosition)
    {
        var (x, y) = currentPosition;
        if (x > 99)
            throw new ArgumentException("Command would move rover out of bounds.");

        return new ValueTuple<int, int>(++x, y);
    }

    private static (int, int) MoveWest((int X, int Y) currentPosition)
    {
        var (x, y) = currentPosition;
        if (x < 1)
            throw new ArgumentException("Command would move rover out of bounds.");

        return new ValueTuple<int, int>(--x, y);
    }
}