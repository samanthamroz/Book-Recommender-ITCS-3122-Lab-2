namespace Lab2;

public interface IRepository {
    public abstract static IRepository Initialize(IItemDatabase items, IUserDatabase users, IRatingMap ratings);
    public abstract static IRepository Instance { get; }
    int GetNextAvailableUserId();
    int GetNextAvailableItemId();
    int GetUserCount();
    int GetItemCount();
    void AddUser(User user);
    void AddItem(Item item);
    User GetUser(int userId);
    Item GetItem(int itemId);
    int GetUsersRatingOfItem(int userId, int itemId);
    void SetUsersRatingOfItem(int userId, int itemId, int rating);
    List<int> GetUsersRatings(int userId);
}