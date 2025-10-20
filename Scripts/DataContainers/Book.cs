namespace Lab2;

public class Book : Item {
    public string Title { get; init; }
    public string Author { get; init; }
    public string YearPublished {get; init; }

    public Book(int itemId, string title, string author, string yearPublished) {
        ItemId = itemId;
        Title = title;
        Author = author;
        YearPublished = yearPublished;
    }
}