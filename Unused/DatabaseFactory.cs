namespace Lab2;

public 

public class DatabaseFactory {
    public DatabaseFactory(I
    public static IDatabase CreateDatabase(string type, string filePath) {
        IDatabase database;

        switch (type) {
            case "BOOK":
                database = ItemDatabaseFactory.CreateDatabase<Book>(filePath);
                break;
            case 
        }
    }
}