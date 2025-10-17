namespace Lab2;

public class ItemDatabaseFactory() {
    public static IItemDatabase CreateDatabase<Item>(string fileName) {
        //get data
        string[] parsedText = FileReader.GetItemFileParsedText(fileName);

        //create reference
        IItemDatabase itemDatabase;

        //fill reference
        if (typeof(Item) == typeof(Book)) {
            itemDatabase = new BookDatabase(parsedText);
        } else {
            throw new ArgumentException();
        }

        //return reference
        return itemDatabase;
    }
}

public class UserDatabaseFactory() {
    public static IUserDatabase CreateDatabase<User>(string fileName) {
        //get data
        List<KeyValuePair<string, int[]>> parsedText = FileReader.GetUserFileParsedText(fileName);

        //create reference
        IUserDatabase database;

        //fill reference
        if (typeof(User) == typeof(Member)) {
            database = new MemberDatabase(parsedText);
        } else {
            throw new ArgumentException();
        }

        //return reference
        return database;
    }
}

public class RatingsDatabaseInitializer() {
    public IRatingsDatabase CreateDatabase<User, Item>(string dataFilePath) {
        //get info
        List<KeyValuePair<string, int[]>> parsedText = FileReader.GetUserFileParsedText(dataFilePath);

        //create reference
        IUserDatabase database;

        //fill reference
        if (typeof(User) == typeof(Member)) {
            database = new MemberDatabase(parsedText);
        } else {
            throw new ArgumentException();
        }

        //return reference
        return database;
    }
}