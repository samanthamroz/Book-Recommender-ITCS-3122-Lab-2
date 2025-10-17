namespace Lab2;

public class Program {
    public static void Main(string[] args) {
        IItemDatabase Books;
        INamedUserDatabase Members;
        IRatingMap Ratings;

        //enter an item filepath
        //what type of item is it? (BOOK)
        Books = new BookDatabase(FileReader.GetItemFileParsedText("file.txt"));

        //enter filepath for user data for those items
        //what type of user? (MEMBER)
        var parsedText = FileReader.GetUserFileParsedText("file.txt");
        Members = new MemberDatabase(parsedText);

        //Create rating map
        Ratings = new RatingMap(Members.MapNamesToIds(parsedText), Books);
    }
}