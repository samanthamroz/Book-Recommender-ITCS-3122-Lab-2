using System;

namespace Lab2;

public class Program {
    static IItemDatabase Items;
    static IUserDatabase Users;
    static IRatingMap Ratings;
    static IRepository repository;
    static ICatalog catalog;
    static ILogger logger;
    static IRecommender recommender;

    public static void Main(string[] args) {
        Console.WriteLine("Welcome to the Book Recommendation Program!");
        Console.WriteLine("Begin setup...");

        DoFillItemsDialogue();
        DoFillUsersAndRatingsDialogue();

        repository = Repository.Initialize(Items, Users, Ratings);
        
        catalog = new Catalog();
        recommender = new Recommender();
        logger = new Logger();

        Console.WriteLine("Setup complete!");
        Console.WriteLine($"- Items added: {Items.GetCount()}");
        Console.WriteLine($"- Users added: {Users.GetCount()}");

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
            Console.WriteLine("Enter item type for this file (BOOK): ");
            string itemType = Console.ReadLine();

            try {
                Items = ItemDatabaseInitializer.NewDatabase(itemFile, itemType);
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
            Console.WriteLine("Enter user type for this file (MEMBER): ");
            string userType = Console.ReadLine();

            var userTextParsed = FileReader.GetUserFileParsedText(userFile);
            try {
                Users = UserDatabaseInitializer.NewDatabase(userFile, userType);
                Ratings = new RatingMap<Member, Book>(Users.MapNamesToIds(userTextParsed), Items);
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
        Console.WriteLine("************** MENU **************\n* 1. Add a new member            *\n* 2. Add a new book              *\n* 3. Rate book                   *\n* 4. View ratings                *\n* 5. See recommendations         *\n* 6. Logout                      *\n**********************************");
    }

    private static void DoLoggedInMenuDialogue() {
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
                catalog.AddItemRating(logger.LoggedInId);
                break;
            case "4":
                catalog.DisplayUsersRatings(logger.LoggedInId);
                break;
            case "5":
                recommender.DisplayRecommendations(logger.LoggedInId);
                break;
            case "6":
                logger.LogOut();
                break;
        }       
    }
}