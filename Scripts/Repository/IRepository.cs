namespace Lab2;

public interface IRepository {
    public abstract static IRepository Initialize(IItemDatabase items, IUserDatabase users, IRatingMap ratings);
    public abstract static IRepository Instance { get; }
    public IItemDatabase GetItemDatabaseOfType(string itemType);
    public IUserDatabase GetUserDatabaseOfType(string userType);
    public IRatingMap GetRatingMapOfType(string userType, string itemType);
}