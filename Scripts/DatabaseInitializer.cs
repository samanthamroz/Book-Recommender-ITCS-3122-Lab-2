namespace Lab2;

public class DatabaseInitializer {
    public static (IUserDatabase members, IRatingMap ratings) InitializeMemberData(string filepath, IItemDatabase books) {
        
        var parsedText = FileReader.GetUserFileParsedText(filepath);
        var memberDb = new MemberDatabase(parsedText);
        var ratingMap = new RatingMap(memberDb.MapNamesToIds(parsedText), books);
        
        return (memberDb, ratingMap);
    }
}