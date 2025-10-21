namespace Lab2;

public class UserDatabase : IUserDatabase {
    List<User> users;

    //This constructor expects the parsed text array to be in the following format:
    //[(memberName, [0, 1, 2, 3... (ratings of all books in order)]), (...,[...])]
    public UserDatabase(List<User> initialUsers) {
        users = initialUsers;
    }

    //Transforms name-based data to ID-based data (for RatingMap)
    public List<KeyValuePair<int, int[]>> MapNamesToIds(List<KeyValuePair<string, int[]>> kvps) {
        List<KeyValuePair<int, int[]>> returnStruct = new();
        
        foreach (var kvp in kvps) {
            int id = GetUserByName(kvp.Key).UserId;
            returnStruct.Add(new(id, kvp.Value));
        }
        
        return returnStruct;
    }

    public void SetUser(User user) {
            //shorthand "for each Member m, m.UserId == member.UserId, remove"
        users.RemoveAll(u => u.UserId == user.UserId);
        
        users.Add(user);
    }

    public void SetUser(List<User> users) {
        foreach (User u in users) {
            SetUser(u);
        }
    }

    public User GetUserById(int id) {
        foreach (User u in users) {
            if (u.UserId == id) {
                return u;
            }
        }
        throw new KeyNotFoundException($"No user found with ID {id}");
    }

    public User GetUserByName(string name) {
        foreach (User u in users) {
            if (u.Name == name) {
                return u;
            }
        }
        throw new KeyNotFoundException($"No user found with name {name}");
    }

    public List<T> GetUsersOfType<T>() where T : User {
        List<T> ofType = new();
        foreach (User user in users) {
            if (user is T typedUser) {
                ofType.Add(typedUser);
            }
        }
        return ofType;
    }

    public int GetCount() {
        return users.Count;
    }

    public int GetNextAvailableId() {
        return users[^1].UserId + 1;
    }
}