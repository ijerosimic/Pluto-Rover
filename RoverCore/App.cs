using RoverCore.Rover;

namespace RoverCore;

public class App
{
    private readonly string[] _args;
    private readonly IOutputPrinter _printer;
    private readonly IRover _rover;

    public App(Arguments args, IOutputPrinter printer, IRover rover)
    {
        _args = args.Args;
        _printer = printer;
        _rover = rover;
    }

    public bool Run()
    {
        if (_args.Length != 1)
        {
            _printer.PrintInvalidArgumentsLength();
            return false;
        }

        if (string.IsNullOrWhiteSpace(_args[0]))
        {
            _printer.PrintInvalidArgument();
            return false;
        }

        try
        {
            _rover.Move(_args[0]);
            _printer.PrintSuccessMessage("Successfully moved the rover.");
        }
        catch (Exception ex)
        {
            _printer.PrintErrorMessage(ex);
            return false;
        }

        return true;
    }
}