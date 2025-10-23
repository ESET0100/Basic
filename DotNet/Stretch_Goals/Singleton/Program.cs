using System;

public class Logger
{
    // Step 1: Private static variable to hold the single instance
    private static Logger _instance;

    // Step 2: Private constructor - prevents creating objects from outside
    private Logger()
    {
        Console.WriteLine("Logger instance created");
    }

    // Step 3: Public static method to get the single instance
    public static Logger GetInstance()
    {
        // Create instance only if it doesn't exist
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    // Regular method
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }
}

class Program
{
    static void Main()
    {
        // Get the logger instance - first time creates it
        Logger logger1 = Logger.GetInstance();
        logger1.Log("First message");

        // Get the logger instance again - returns same instance
        Logger logger2 = Logger.GetInstance();
        logger2.Log("Second message");

        // Check if both variables point to the same object
        Console.WriteLine($"Same instance? {logger1 == logger2}");

        // Try to create another instance - won't work due to private constructor
        // Logger logger3 = new Logger(); // This would cause compile error
    }
}