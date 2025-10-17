namespace Lab2;

public interface IUserDatabase : IDatabase {
    void SetUser(User user);
    User GetUserById(int id);
}