namespace Lab2;

public class ItemDatabaseInitializer {
    public static IItemDatabase NewDatabase(string itemFile, string type) {

        var itemTextParsed = FileReader.GetItemFileParsedText(itemFile);
        switch (type.ToUpperInvariant()) {
            case "BOOK":
                return new BookDatabase(itemTextParsed);
            default:
                throw new ArgumentException();
        }
    }
}