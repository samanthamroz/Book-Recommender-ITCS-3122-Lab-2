namespace Lab2;

public class UserDatabaseInitializer {
    public static IDatabase NewDatabase(string userFile, string type) {
        var userTextParsed = FileReader.GetUserFileParsedText(userFile);
        switch (type.ToUpperInvariant()) {
            case "MEMBER":
                return new MemberDatabase(userTextParsed);
            default:
                throw new ArgumentException();
        }
    }
}