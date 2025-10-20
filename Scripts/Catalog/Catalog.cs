using System.Data;

namespace Lab2;

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
        Console.WriteLine($"New {type} successfully added with ID {id}. Current {type} count: {database.GetCount()}");
    }

    public User GetUserById(string type, int id) {
        IUserDatabase database = Repository.Instance.GetUserDatabaseOfType(type);
        return database.GetUserById(id);
    }

    public void AddItem() {
        Console.WriteLine($"Enter type of item to add ({Item.GetItemTypes()}): ");
        string type = Console.ReadLine();
        type = type.ToLowerInvariant();

        IItemDatabase database;
        try {
            database = Repository.Instance.GetItemDatabaseOfType(type);
        } catch (ArgumentException) {
            Console.WriteLine($"Error: Database of type {type} does not exist.");
            return;
        }
        
        int id = database.GetNextAvailableId();

        Item newItem = ItemFactory.CreateItemFromConsoleInput(type, id);
        database.SetItem(newItem);
        
        Console.WriteLine($"New {type} successfully added with ID {id}. Current {type} count: {database.GetCount()}");
    }

    public void AddItemRating(int userId) {
        
    }

    public void DisplayUsersRatings(int userId) {
        
    }
}