namespace Lab2;

public class UserDatabaseFactory {
    public static IUserDatabase NewDatabaseFromFile(string userFile, string type) {
        IFileReader fileReader = new StandardFileReader();
        var userTextParsed = fileReader.GetUserFileParsedText(userFile);

        List<User> initialUsers = new();
        int id;
        try {
            id = Repository.Instance.GetNextAvailableUserId();
        } catch (InvalidOperationException) {
            id = 0;
        }

        switch (type.ToUpperInvariant()) {
            case "MEMBER":
                //This constructor expects the parsed text array to be in the following format:
                //[(memberName, [0, 1, 2, 3... (ratings of all books in order)]), (...,[...])]
                for (int i = 0; i < userTextParsed.Count; i++) {
                    Member newMember = new(id, userTextParsed[i].Key);
                    initialUsers.Add(newMember);
                    id++;
                }
                break;
            default:
                throw new ArgumentException();
        }

        return new UserDatabase(initialUsers);
    }
}