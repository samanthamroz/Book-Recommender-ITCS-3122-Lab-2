namespace Lab2;

public static class ItemFactory {
    public static Item CreateItemFromConsoleInput(string itemType, int id) {
        switch (itemType.ToUpperInvariant()) {
            case "BOOK":
                Console.WriteLine("Enter book title: ");
                string title = Console.ReadLine();
                Console.WriteLine("Enter book author: ");
                string author = Console.ReadLine();
                Console.WriteLine("Enter publication year: ");
                string year = Console.ReadLine();
                
                return new Book(id, title, author, year);
            default:
                throw new ArgumentException($"Unknown item type: {itemType}");
        }
    }
}