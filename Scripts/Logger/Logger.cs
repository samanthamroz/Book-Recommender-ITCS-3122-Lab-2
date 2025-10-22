namespace Lab2;

public class Logger : ILogger {
    public int LoggedInId { get; private set; }
    public User? LoggedInUser { get; private set; }
    private IUserRepository _userRepository;
    public Logger(IUserRepository userRepository) {
        _userRepository = userRepository;
        LoggedInId = -1;
        LoggedInUser = null;
    }

    public void LogIn() {
        Console.WriteLine("Enter your member ID: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int id)) {
            Console.WriteLine($"Input {input} not valid as an ID.");
            Console.WriteLine("----------");
            return;
        }
        
        try {
            LoggedInUser = _userRepository.GetUser(id);
            LoggedInId = id;
        } catch (KeyNotFoundException) {
            Console.WriteLine($"No user with ID {id} found");
        }
        Console.WriteLine("----------");
    }

    public void LogOut() {
        Console.WriteLine($"You have been successfully logged out.");
        Console.WriteLine("----------");
        LoggedInId = -1;
        LoggedInUser = null;
    }

    public bool IsLoggedIn() {
        return LoggedInId > -1;
    }
}