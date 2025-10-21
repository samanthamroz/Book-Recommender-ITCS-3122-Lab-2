namespace Lab2;

public class Logger : ILogger {
    public int LoggedInId { get; private set; }
    public User? LoggedInUser { get; private set; }
    public Logger() {
        LoggedInId = -1;
        LoggedInUser = null;
    }

    public void LogIn() {
        Console.WriteLine("Enter your member ID: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int id)) {
            Console.WriteLine($"Input {input} not valid as an ID.");
            return;
        }
        
        try {
            LoggedInUser = Repository.Instance.GetUser(id);
            LoggedInId = id;
        } catch (KeyNotFoundException) {
            Console.WriteLine($"No user with ID {id} found");
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