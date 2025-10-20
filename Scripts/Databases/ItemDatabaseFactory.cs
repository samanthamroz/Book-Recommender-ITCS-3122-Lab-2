namespace Lab2;

public class ItemDatabaseFactory {
    public static IItemDatabase NewDatabaseFromFile(string itemFile, string type) {
        IFileReader fileReader = new StandardFileReader();
        
        var itemTextParsed = fileReader.GetItemFileParsedText(itemFile);
        switch (type.ToUpperInvariant()) {
            case "BOOK":
                return new BookDatabase(itemTextParsed);
            default:
                throw new ArgumentException();
        }
    }
}