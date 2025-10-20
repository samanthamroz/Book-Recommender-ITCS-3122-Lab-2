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
        Console.WriteLine($"Enter type of user to add ({User.GetUserTypes()}): ");
        string type = Console.ReadLine();
        type = type.ToLowerInvariant();

        IUserDatabase database;
        try {
            database = Repository.Instance.GetUserDatabaseOfType(type);
        } catch (ArgumentException) {
            Console.WriteLine($"Error: Database of type {type} does not exist.");
            return;
        }
        
        int id = database.GetNextAvailableId();

        Console.WriteLine($"Enter the new {type}'s name: ");
        string name = Console.ReadLine();

        Member newMember = new(id, name);
        database.SetUser(newMember);
        Console.WriteLine($"New {type} successfully added. Current {type} count: {database.GetCount()}");
    }

    public void AddItem() {
        Console.WriteLine($"Enter type of item to add ({Item.GetItemTypes()}): ");
        string type = Console.ReadLine();
        type = type.ToLowerInvariant();

        IUserDatabase database;
        try {
            database = Repository.Instance.GetUserDatabaseOfType(type);
        } catch (ArgumentException) {
            Console.WriteLine($"Error: Database of type {type} does not exist.");
            return;
        }
        
        int id = database.GetNextAvailableId();

        Console.WriteLine($"Enter the new {type}'s name: ");
        string name = Console.ReadLine();

        Member newMember = new(id, name);
        database.SetUser(newMember);
        Console.WriteLine($"New {type} successfully added. Current {type} count: {database.GetCount()}");
    }

    public void AddItemRating(int userId) {
        
    }

    public void DisplayUsersRatings(int userId) {
        
    }
}