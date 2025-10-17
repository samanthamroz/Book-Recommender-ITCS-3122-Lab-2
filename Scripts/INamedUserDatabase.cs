namespace Lab2;

public interface INamedUserDatabase : IUserDatabase {
    User GetUserByName(string name);
    List<KeyValuePair<int, int[]>> MapNamesToIds(List<KeyValuePair<string, int[]>> kvps);
}