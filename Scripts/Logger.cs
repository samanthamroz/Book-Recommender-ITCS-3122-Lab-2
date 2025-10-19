namespace Lab2;

public interface ILogger {
    public int LoggedInId { get; }
    public void LogIn();
    public void LogOut();
    public bool IsLoggedIn();
}

public class Logger : ILogger {
    public int LoggedInId { get; private set; }
    public Logger() {
        LoggedInId = -1;
    }

    public void LogIn() {
        Console.WriteLine("Enter your member ID: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out _)) {
            Console.WriteLine($"Member id {input} not found. Unable to log in.");
            return;   
        } else {
            LoggedInId = int.Parse(input);
        }
    }

    public void LogOut() {
        Console.WriteLine($"You have been successfully logged out.");
        LoggedInId = -1;
    }

    public bool IsLoggedIn() {
        return LoggedInId > -1;
    }
}