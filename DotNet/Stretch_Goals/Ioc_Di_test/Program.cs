using System;

// Simple interfaces
public interface IMessageService
{
    void Send(string message);
}

public interface ILogger
{
    void Log(string message);
}

// Implementations
public class EmailService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending email: {message}");
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}

// Main class with both injection types
public class NotificationService
{
    // Constructor injection (required)
    private readonly IMessageService _messageService;

    // Property injection (optional)
    public ILogger Logger { get; set; }

    // Constructor - message service is required
    public NotificationService(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(string message)
    {
        // Use property-injected logger if available
        if (Logger != null)
        {
            Logger.Log($"Starting notification: {message}");
        }

        // Use constructor-injected message service
        _messageService.Send(message);

        if (Logger != null)
        {
            Logger.Log("Notification completed");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Constructor Injection ===");
        EmailService emailService = new EmailService();
        NotificationService notification = new NotificationService(emailService);
        notification.Notify("Hello with constructor injection");

        Console.WriteLine("\n=== Property Injection ===");
        ConsoleLogger logger = new ConsoleLogger();
        notification.Logger = logger; // Set property
        notification.Notify("Hello with both injections");

        Console.WriteLine("\n=== Remove Property Injection ===");
        notification.Logger = null; // Remove logger
        notification.Notify("Hello without logger");
    }
}