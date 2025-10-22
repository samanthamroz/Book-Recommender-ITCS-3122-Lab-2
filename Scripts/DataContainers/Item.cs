namespace Lab2;

public abstract class Item {
    public int ItemId { get; init; }
    public abstract override string ToString();
    public static string GetItemTypes() {
        return "BOOK";
    }
}