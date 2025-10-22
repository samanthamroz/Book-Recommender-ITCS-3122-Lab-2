namespace Lab2;

public class UserRepository : IUserRepository {
    IUserDatabase _userDatabase;
    public UserRepository(IUserDatabase users) {
        _userDatabase = users;
    }
    public int GetNextAvailableUserId() {
        return _userDatabase.GetNextAvailableId();
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