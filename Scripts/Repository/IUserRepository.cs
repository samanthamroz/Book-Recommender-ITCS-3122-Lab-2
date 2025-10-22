namespace Lab2;

public interface IUserRepository {
    int GetNextAvailableUserId();
    int GetUserCount();
    void AddUser(User user);
    User GetUser(int userId);
}