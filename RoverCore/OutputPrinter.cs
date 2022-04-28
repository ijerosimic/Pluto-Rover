namespace RoverCore;

public interface IOutputPrinter
{
    void PrintInvalidArgumentsLength();
    void PrintInvalidArgument();
    void PrintSuccessMessage(string message);
    void PrintErrorMessage(Exception ex);
}

public class OutputPrinter : IOutputPrinter
{
    public void PrintInvalidArgumentsLength()
    {
        Console.WriteLine("Invalid arguments length.");
    }

    public void PrintInvalidArgument()
    {
        Console.WriteLine("Argument cannot be null or empty.");
    }

    public void PrintSuccessMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintErrorMessage(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}