namespace Lab2;

public class UserDatabaseFactory {
    public static IUserDatabase NewDatabaseFromFile(string userFile, string type, int nextAvailableId) {
        IFileReader fileReader = new StandardFileReader();
        var userTextParsed = fileReader.GetUserFileParsedText(userFile);

        List<User> initialUsers = new();

        switch (type.ToUpperInvariant()) {
            case "MEMBER":
                //This constructor expects the parsed text array to be in the following format:
                //[(memberName, [0, 1, 2, 3... (ratings of all books in order)]), (...,[...])]
                for (int i = 0; i < userTextParsed.Count; i++) {
                    Member newMember = new(nextAvailableId, userTextParsed[i].Key);
                    initialUsers.Add(newMember);
                    nextAvailableId++;
                }
                break;
            default:
                throw new ArgumentException();
        }

        return new UserDatabase(initialUsers);
    }
}