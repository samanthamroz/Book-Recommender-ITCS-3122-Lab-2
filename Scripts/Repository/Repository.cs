namespace Lab2;

public class Repository : IRepository {
    IItemDatabase _itemDatabase;
    IUserDatabase _userDatabase;
    IRatingMap _ratingMap;
    private static IRepository? self;
    private Repository(IItemDatabase items, IUserDatabase users, IRatingMap ratings) {
        _itemDatabase = items;
        _userDatabase = users;
        _ratingMap = ratings;
    }

    public static IRepository Initialize(IItemDatabase items, IUserDatabase users, IRatingMap ratings) {
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

    public int GetNextAvailableUserId() {
        try {
            return _userDatabase.GetNextAvailableId();
        } catch (IndexOutOfRangeException) {
            return 0;
        }
    }
    public int GetNextAvailableItemId() {
        try {
            return _itemDatabase.GetNextAvailableId();
        } catch (IndexOutOfRangeException) {
            return 0;
        }
    }

    public int GetUserCount() {
        return _userDatabase.GetCount();
    }
    public int GetItemCount() {
        return _itemDatabase.GetCount();
    }

    public void AddUser(User user) {
        _userDatabase.SetUser(user);
    }
    public void AddItem(Item item) {
        _itemDatabase.SetItem(item);
    }

    public User GetUser(int userId) {
        return _userDatabase.GetUserById(userId);
    }
    public Item GetItem(int itemId) {
        return _itemDatabase.GetItemById(itemId);
    }

    public int GetUsersRatingOfItem(int userId, int itemId) {
        return _ratingMap.GetItemRating(userId, itemId);
    }
    public void SetUsersRatingOfItem(int userId, int itemId, int rating) {
        _ratingMap.SetItemRating(userId, itemId, rating);
    }
}