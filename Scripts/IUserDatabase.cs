namespace Lab2;

public interface IUserDatabase : IDatabase {
    void SetUser(User user);
    User GetUserById(int id);
    User GetUserByName(string name);
    List<KeyValuePair<int, int[]>> MapNamesToIds(List<KeyValuePair<string, int[]>> kvps);
    int GetNextAvailableId();
}