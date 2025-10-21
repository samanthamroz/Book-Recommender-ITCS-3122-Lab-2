namespace Lab2;

public static class UserFactory {
    public static User CreateUserFromConsoleInput(string userType, int id) {
        switch (userType.ToUpperInvariant()) {
            case "MEMBER":
                Console.WriteLine("Enter member name: ");
                string name = Console.ReadLine();
                
                return new Member(id, name);
            default:
                throw new ArgumentException($"Unknown user type: {userType}");
        }
    }
}