namespace Lab2;

public class BookDatabase : IItemDatabase {
    List<Book> books;

    //This constructor expects the parsed text array to be in the following format:
    //[book1author, book1title, book1year, book2author, book2title, book2year, etc]
    public BookDatabase(string[] parsedText) {
        books = new();

        for (int i = 0; i < parsedText.Length; i += 3) {
            Book book = new(i, parsedText[i + 1], parsedText[i], int.Parse(parsedText[i + 2]));
            books.Add(book);
        }
    }
    
    public Item GetItemById(int id) {
        throw new NotImplementedException();
    }

    public void SetItem(Item item) {
        throw new NotImplementedException();
    }
}