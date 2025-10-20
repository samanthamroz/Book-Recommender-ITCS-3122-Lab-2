namespace Lab2;

public class BookDatabase : IItemDatabase {
    List<Book> books;

    //This constructor expects the parsed text array to be in the following format:
    //[book1author, book1title, book1year, book2author, book2title, book2year, etc]
    public BookDatabase(string[] parsedText) {
        books = new();

        for (int i = 0; i < parsedText.Length; i += 3) {
            Book book = new(i, parsedText[i + 1], parsedText[i], parsedText[i + 2]);
            books.Add(book);
        }
    }

    public int GetCount() {
        return books.Count;
    }

    public int GetNextAvailableId() {
        return books[^1].ItemId + 1;
    }

    public void SetItem(Item item) {
        if (item is not Book book) {
            throw new ArgumentException("BookDatabase can only store Book objects");
        }
            //shorthand "for each Member m, m.UserId == member.UserId, remove"
        books.RemoveAll(b => b.ItemId == book.ItemId);
        
        books.Add(book);
    }

    public Item GetItemById(int id) {
        foreach (Book b in books) {
            if (b.ItemId == id) {
                return b;
            }
        }
        throw new KeyNotFoundException($"No book found with ID {id}");
    }
}