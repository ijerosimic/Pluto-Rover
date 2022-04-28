namespace RoverCore.Rover;

public interface IRover
{
    void Move(string command);
}

public class Rover : IRover
{
    public string GetCurrentCoordinates() => _roverMovements.Last().ToString();

    public void Move(string command)
    {
        var actions = command.Select(x => x).ToList();
        foreach (var action in actions)
        {
            var currentDirection = _roverMovements.Last().Direction;

            if (!PossibleActions[currentDirection].ContainsKey(action))
                throw new ArgumentOutOfRangeException(nameof(command), "Unrecognized rover move command.");

            PossibleActions[currentDirection][action].Invoke();
        }
    }

    private readonly List<(int X, int Y, char Direction)> _roverMovements = new() { (0, 0, Directions.North) };

    private Dictionary<char, Dictionary<char, Action>> PossibleActions => new()
    {
        {
            Directions.North, new Dictionary<char, Action>
            {
                { Commands.Forward, MoveNorth },
                { Commands.Backward, MoveSouth },
                { Commands.Right, TurnRight },
                { Commands.Left, TurnLeft }
            }
        },
        {
            Directions.East, new Dictionary<char, Action>
            {
                { Commands.Forward, MoveEast },
                { Commands.Backward, MoveWest },
                { Commands.Right, TurnRight },
                { Commands.Left, TurnLeft }
            }
        },
        {
            Directions.South, new Dictionary<char, Action>
            {
                { Commands.Forward, MoveSouth },
                { Commands.Backward, MoveNorth },
                { Commands.Right, TurnRight },
                { Commands.Left, TurnLeft }
            }
        },
        {
            Directions.West, new Dictionary<char, Action>
            {
                { Commands.Forward, MoveWest },
                { Commands.Backward, MoveEast },
                { Commands.Right, TurnRight },
                { Commands.Left, TurnLeft }
            }
        }
    };

    private void TurnLeft() => Turn(Commands.Left);
    private void TurnRight() => Turn(Commands.Right);

    private void Turn(char orientation)
    {
        var (x, y, direction) = _roverMovements.Last();
        var newDirection = Directions.Turn(direction, orientation);
        _roverMovements.Add(new ValueTuple<int, int, char>(x, y, newDirection));
    }

    private void MoveNorth()
    {
        var (x, y, direction) = _roverMovements.Last();
        if (y > 99)
            throw new ArgumentException("Command would move rover out of bounds.");

        _roverMovements.Add(new ValueTuple<int, int, char>(x, ++y, direction));
    }

    private void MoveSouth()
    {
        var (x, y, direction) = _roverMovements.Last();
        if (y < 1)
            throw new ArgumentException("Command would move rover out of bounds.");

        _roverMovements.Add(new ValueTuple<int, int, char>(x, --y, direction));
    }

    private void MoveEast()
    {
        var (x, y, direction) = _roverMovements.Last();
        if (x > 99)
            throw new ArgumentException("Command would move rover out of bounds.");

        _roverMovements.Add(new ValueTuple<int, int, char>(++x, y, direction));
    }

    private void MoveWest()
    {
        var (x, y, direction) = _roverMovements.Last();
        if (x < 1)
            throw new ArgumentException("Command would move rover out of bounds.");

        _roverMovements.Add(new ValueTuple<int, int, char>(--x, y, direction));
    }
}