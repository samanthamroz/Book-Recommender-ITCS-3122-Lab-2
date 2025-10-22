using System;

namespace Lab2;

public class Program {
    static IItemDatabase items;
    static IUserDatabase users;
    static IRatingMap ratings;
    static RepositoryCollection repositories;
    static ICatalog catalog;
    static ILogger logger;
    static IRecommender recommender;

    public static void Main(string[] args) {
        Console.WriteLine("Welcome to the Book Recommendation Program!");
        Console.WriteLine("Begin setup...");

        IItemRepository itemRepository = new ItemRepository(items);
        IUserRepository userRepository = new UserRepository(users);
        IRatingMapRepository ratingMapRepository = new RatingMapRepository(ratings);
        repositories = new RepositoryCollection(itemRepository, userRepository, ratingMapRepository);

        DoFillItemsDialogue();
        DoFillUsersAndRatingsDialogue();
        
        catalog = new Catalog(repositories);
        recommender = new Recommender(repositories);
        logger = new Logger(repositories.UserRepository);

        Console.WriteLine("Setup complete!");
        Console.WriteLine($"- Items added: {items.GetCount()}");
        Console.WriteLine($"- Users added: {users.GetCount()}");

        while (true) { //Environment.Exit contained in the guest menu dialogue
            if (!logger.IsLoggedIn()) {
                DoGuestMenuDialogue();
            } else {
                DoLoggedInMenuDialogue(); 
            }
        }
    }

    private static void DoFillItemsDialogue() {
        while (true) {
            Console.WriteLine("Enter item file path: ");
            string itemFile = Console.ReadLine();
            Console.WriteLine($"Enter item type for this file ({ItemFactory.GetItemTypes()}): ");
            string itemType = Console.ReadLine();

            try {
                items = ItemDatabaseFactory.NewDatabaseFromFile(itemFile, itemType, repositories.ItemRepository.GetNextAvailableItemId());
                return;
            } catch (ArgumentException) {
                Console.WriteLine("Invalid inputs. Please try again.");
            }
        }
    }

    private static void DoFillUsersAndRatingsDialogue() {
        while (true) {
            Console.WriteLine("Enter user file path: ");
            string userFile = Console.ReadLine();
            Console.WriteLine($"Enter user type for this file ({UserFactory.GetUserTypes()}): ");
            string userType = Console.ReadLine();

            IFileReader fileReader = new StandardFileReader();
            var userTextParsed = fileReader.GetUserFileParsedText(userFile);
            try {
                users = UserDatabaseFactory.NewDatabaseFromFile(userFile, userType, repositories.UserRepository.GetNextAvailableUserId());
                ratings = new RatingMap(users.MapNamesToIds(userTextParsed), items);
                return;
            } catch (ArgumentException) {
                Console.WriteLine("Invalid inputs. Please try again.");
            }
        }
    }

    private static void PrintGuestMenu() {
        Console.WriteLine("************** MENU **************\n* 1. Add a new member            *\n* 2. Add a new book              *\n* 3. Login                       *\n* 4. Quit                        *\n**********************************");
    }

    private static void DoGuestMenuDialogue() {
        PrintGuestMenu();
        string input = Console.ReadLine();

        switch (input) {
            case "1":
                catalog.AddUser();
                break;
            case "2":
                catalog.AddItem();
                break;
            case "3":
                logger.LogIn();
                break;
            case "4":
                Environment.Exit(1);
                break;
            default:
                break;
        }
    }

    private static void PrintMemberMenu() {
        Console.WriteLine("****************** MENU *******************\n* 1. Add a new member                     *\n* 2. Add a new book                       *\n* 3. Rate book                            *\n* 4. View ratings                         *\n* 5. See your top recommendations         *\n* 6. See a similar user's recommendations *\n* 7. Logout                               *\n*******************************************");
    }

    private static void DoLoggedInMenuDialogue() {
        User loggedInUser = logger.LoggedInUser;
        Console.WriteLine($"Welcome, {loggedInUser.Name}");
        PrintMemberMenu();
        string input = Console.ReadLine();

        switch (input) {
            case "1":
                catalog.AddUser();
                break;
            case "2":
                catalog.AddItem();
                break;
            case "3":
                catalog.AddItemRating(logger.LoggedInUser);
                break;
            case "4":
                catalog.DisplayUsersRatings(logger.LoggedInUser);
                break;
            case "5":
                recommender.DisplayGlobalRecommendations(logger.LoggedInId);
                break;
            case "6":
                recommender.DisplaySingleSimilarUserRecommendations(logger.LoggedInId);
                break;
            case "7":
                logger.LogOut();
                break;
        }       
    }
}