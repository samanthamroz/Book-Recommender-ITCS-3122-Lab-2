namespace Lab2;

public class UserDatabaseFactory {
    public static IUserDatabase NewDatabaseFromFile(string userFile, string type) {
        IFileReader fileReader = new StandardFileReader();
        var userTextParsed = fileReader.GetUserFileParsedText(userFile);
        switch (type.ToUpperInvariant()) {
            case "MEMBER":
                return new MemberDatabase(userTextParsed);
            default:
                throw new ArgumentException();
        }
    }
}