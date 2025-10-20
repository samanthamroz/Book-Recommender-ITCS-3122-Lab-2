namespace Lab2;

public interface IRepository {
    public abstract static IRepository Initialize(IItemDatabase items, IUserDatabase<User> users, IRatingMap ratings);
    public abstract static IRepository Instance { get; }
    public IItemDatabase GetItemDatabaseOfType(string itemType);
    public IUserDatabase<User> GetUserDatabaseOfType(string userType);
    public IRatingMap GetRatingMapOfType(string userType, string itemType);
}

public class Repository : IRepository {
    List<IItemDatabase> ItemDatabases;
    List<IUserDatabase<User>> UserDatabases;
    List<IRatingMap> RatingMaps;
    private static IRepository? self;
    private Repository(IItemDatabase items, IUserDatabase<User> users, IRatingMap ratings) {
        ItemDatabases = [items];
        UserDatabases = [users];
        RatingMaps = [ratings];
    }

    public static IRepository Initialize(IItemDatabase items, IUserDatabase<User> users, IRatingMap ratings) {
        if (self == null) {
            self = new Repository(items, users, ratings);
        }
        return self;
    }
    public static IRepository Instance {
        get {
            if (self == null) {
                throw new InvalidOperationException("Repository must be initialized before use. Call Initialize() first.");
            }
            return self;
        }
    }

    public IItemDatabase GetItemDatabaseOfType(string itemType) {
        switch (itemType.ToUpperInvariant()) {
            case "BOOK":
                foreach (IItemDatabase db in ItemDatabases) {
                    if (db is BookDatabase) {
                        return db;
                    }
                }
                break;
        }
        throw new ArgumentException($"No database of type {itemType} exists");
    }

    public IUserDatabase<User> GetUserDatabaseOfType(string userType) {
        switch (userType.ToUpperInvariant()) {
            case "MEMBER":
                foreach (IUserDatabase<User> db in UserDatabases) {
                    if (db is MemberDatabase) {
                        return db;
                    }
                }
                break;
        }
        throw new ArgumentException($"No database of type {userType} exists");
    }

    public IRatingMap GetRatingMapOfType(string userType, string itemType) {
        Type userTypeObj = GetTypeFromString(userType);
        Type itemTypeObj = GetTypeFromString(itemType);
        
        if (userTypeObj == null || itemTypeObj == null) {
            throw new ArgumentException($"Invalid user type '{userType}' or item type '{itemType}'");
        }
        
        Type targetRatingMapType = typeof(RatingMap<,>).MakeGenericType(userTypeObj, itemTypeObj);
        
        foreach (IRatingMap map in RatingMaps) {
            if (targetRatingMapType.IsInstanceOfType(map)) {
                return map;
            }
        }
        
        throw new ArgumentException($"No rating map of type RatingMap<{userType}, {itemType}> exists");
    }

    private Type? GetTypeFromString(string typeName) {
        return typeName.ToUpperInvariant() switch {
            "MEMBER" => typeof(Member),
            "BOOK" => typeof(Book),
            _ => null
        };
    }
}