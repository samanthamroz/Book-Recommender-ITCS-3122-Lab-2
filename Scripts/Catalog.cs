using System.Data;

namespace Lab2;

public interface ICatalog {
    public void AddUser();
    public void AddItem();
    public void AddItemRating(int userId);
    public void DisplayUsersRatings(int userId);
}

public class Catalog : ICatalog {
    public Catalog() {
        
    }

    public void AddUser() {
        Console.WriteLine("Enter type of user to add (MEMBER): ");
        string type = Console.ReadLine();

        IUserDatabase<User> database;
        try {
            database = Repository.Instance.GetUserDatabaseOfType(type);
        } catch (ArgumentException) {
            Console.WriteLine($"Error: Database of type {type} does not exist.");
            return;
        }
        
        int id = database.GetNextAvailableId();

        Console.WriteLine($"Enter the new {type.ToLowerInvariant()}'s name: ");
        string name = Console.ReadLine();

        Member newMember = new(id, name);
        database.SetUser(newMember);
    }

    public void AddItem() {
        
    }

    public void AddItemRating(int userId) {
        
    }

    public void DisplayUsersRatings(int userId) {
        
    }
}