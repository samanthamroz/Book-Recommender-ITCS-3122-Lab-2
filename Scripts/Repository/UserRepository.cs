namespace Lab2;

public class UserRepository : IUserRepository {
    IUserDatabase _userDatabase;
    public UserRepository(IUserDatabase users) {
        _userDatabase = users;
    }
    public int GetNextAvailableUserId() {
        try {
            return _userDatabase.GetNextAvailableId();
        } catch (IndexOutOfRangeException) {
            return 0;
        }
    }

    public int GetUserCount() {
        return _userDatabase.GetCount();
    }

    public void AddUser(User user) {
        _userDatabase.SetUser(user);
    }

    public User GetUser(int userId) {
        return _userDatabase.GetUserById(userId);
    }
}