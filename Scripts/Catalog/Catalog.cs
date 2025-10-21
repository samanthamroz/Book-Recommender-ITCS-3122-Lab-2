using System.Data;

namespace Lab2;

public class Catalog : ICatalog {
    public void AddUser() {
        Console.WriteLine($"Enter type of user to add ({User.GetUserTypes()}): ");
        string type = Console.ReadLine();
        type = type.ToLowerInvariant();
        
        int id = Repository.Instance.GetNextAvailableUserId();
        try {
            User newUser = UserFactory.CreateUserFromConsoleInput(type, id);
            Repository.Instance.AddUser(newUser);

            Console.WriteLine($"New {type} successfully added with ID {id}. Current user count: {Repository.Instance.GetUserCount()}");
        } catch (ArgumentException) {
            Console.WriteLine("Invalid inputs for creating a new user.");
        }
    }

    public void AddItem() {
        Console.WriteLine($"Enter type of item to add ({Item.GetItemTypes()}): ");
        string type = Console.ReadLine();
        type = type.ToLowerInvariant();

        int id = Repository.Instance.GetNextAvailableItemId();
        try {
            Item newItem = ItemFactory.CreateItemFromConsoleInput(type, id);
            Repository.Instance.AddItem(newItem);
        
            Console.WriteLine($"New {type} successfully added with ID {id}. Current {type} count: {Repository.Instance.GetItemCount()}");
        } catch (ArgumentException) {
            Console.WriteLine("Invalid inputs for creating a new item.");
        }
    }

    public void AddItemRating(User loggedInUser) {
        Console.WriteLine("Enter the ID of the item you want to rate: ");
        string itemId = Console.ReadLine();
        Item itemFound;

        try {
            itemFound = Repository.Instance.GetItem(int.Parse(itemId));
        } catch (FormatException) {
            Console.WriteLine($"Input {itemId} is not valid as an item ID.");
            return;
        } catch (KeyNotFoundException) {
            Console.WriteLine($"Item with ID {itemId} does not exist.");
            return;
        }
        
        Console.WriteLine($"Enter your rating for {itemFound}: ");
        string input = Console.ReadLine();

        int rating;
        try {
            rating = int.Parse(input);
        } catch (FormatException) {
            Console.WriteLine($"Input {input} is not valid as a rating.");
            return;
        }

        try {
            Repository.Instance.SetUsersRatingOfItem(loggedInUser.UserId, itemFound.ItemId, rating);
        } catch (ArgumentException) {
            Console.WriteLine($"Ratings must be one of the following: -5, -3, 0, 1, 3, 5");
            return;
        }

        Console.WriteLine($"You rated {itemFound} a {rating}!");
    }

    public void DisplayUsersRatings(User loggedInUser) {
        IRepository repo = Repository.Instance;
        Console.WriteLine("Your ratings:\n----------");
        for (int i = 0; i < repo.GetNextAvailableItemId(); i++) {
            try {
                Item itemFound = repo.GetItem(i);
                int rating = repo.GetUsersRatingOfItem(loggedInUser.UserId, i);
                Console.WriteLine($"ID: {itemFound.ItemId} || Your Rating: {rating} || {itemFound}");
            } catch (KeyNotFoundException) {
                
            }
        }
        Console.WriteLine("----------");
    }
}