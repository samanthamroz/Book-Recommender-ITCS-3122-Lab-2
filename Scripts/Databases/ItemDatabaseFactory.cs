namespace Lab2;

public class ItemDatabaseFactory {
    public static IItemDatabase NewDatabaseFromFile(string itemFile, string type) {
        IFileReader fileReader = new StandardFileReader();
        
        var itemTextParsed = fileReader.GetItemFileParsedText(itemFile);

        List<Item> initialItems = new();
        int id = 0;
        if (Repository.IsInitialized) {
            id = Repository.Instance.GetNextAvailableItemId();
        }

        switch (type.ToUpperInvariant()) {
            case "BOOK":
                for (int i = 0; i < itemTextParsed.Length; i += 3) {
                    Book book = new(id, itemTextParsed[i + 1], itemTextParsed[i], itemTextParsed[i + 2]);
                    initialItems.Add(book);
                    id++;
                }
                break;
            default:
                throw new ArgumentException();
        }

        return new ItemDatabase(initialItems);
    }
}