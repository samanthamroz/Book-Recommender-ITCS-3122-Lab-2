using System.Data;

namespace Lab2;

public class Catalog : ICatalog {
    private IRatingMapRepository _ratingMapRepository;
    private IUserRepository _userRepository;
    private IItemRepository _itemRepository;
    public Catalog(RepositoryCollection repositoryCollection) {
        _ratingMapRepository = repositoryCollection.RatingMapRepository;
        _userRepository = repositoryCollection.UserRepository;
        _itemRepository = repositoryCollection.ItemRepository;
    } 

    public void AddUser() {
        Console.WriteLine($"Enter type of user to add ({UserFactory.GetUserTypes()}): ");
        string type = Console.ReadLine();
        type = type.ToLowerInvariant();
        
        int id = _userRepository.GetNextAvailableUserId();
        try {
            User newUser = UserFactory.CreateUserFromConsoleInput(type, id);
            _userRepository.AddUser(newUser);

            Console.WriteLine($"New {type} successfully added with ID {id}. Current user count: {_userRepository.GetUserCount()}");
        } catch (ArgumentException) {
            Console.WriteLine("Invalid inputs for creating a new user.");
        }
        Console.WriteLine("----------");
    }

    public void AddItem() {
        Console.WriteLine($"Enter type of item to add ({ItemFactory.GetItemTypes()}): ");
        string type = Console.ReadLine();
        type = type.ToLowerInvariant();

        int id = _itemRepository.GetNextAvailableItemId();
        try {
            Item newItem = ItemFactory.CreateItemFromConsoleInput(type, id);
            _itemRepository.AddItem(newItem);
            _ratingMapRepository.ResetAllRatingsOfItem(newItem.ItemId);
            Console.WriteLine($"New {type} successfully added with ID {id}. Current {type} count: {_itemRepository.GetItemCount()}");
        } catch (ArgumentException) {
            Console.WriteLine("Invalid inputs for creating a new item.");
        }
        Console.WriteLine("----------");
    }

    public void AddItemRating(User loggedInUser) {
        Console.WriteLine("Enter the ID of the item you want to rate: ");
        string itemId = Console.ReadLine();
        Item itemFound;

        try {
            itemFound = _itemRepository.GetItem(int.Parse(itemId));
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
            _ratingMapRepository.SetUsersRatingOfItem(loggedInUser.UserId, itemFound.ItemId, rating);
        } catch (ArgumentException) {
            Console.WriteLine($"Ratings must be one of the following: -5, -3, 0, 1, 3, 5");
            return;
        }

        Console.WriteLine($"You rated {itemFound} a {rating}!");
        Console.WriteLine("----------");
    }

    public void DisplayUsersRatings(User loggedInUser) {
        Console.WriteLine("Your ratings:\n----------");
        for (int i = 0; i < _itemRepository.GetNextAvailableItemId(); i++) {
            try {
                Item itemFound = _itemRepository.GetItem(i);
                int rating = _ratingMapRepository.GetUsersRatingOfItem(loggedInUser.UserId, i);
                Console.WriteLine($"ID: {itemFound.ItemId} || Your Rating: {rating} || {itemFound}");
            } catch (KeyNotFoundException) {
                
            }
        }
        Console.WriteLine("----------");
    }
}