namespace Lab2;

public abstract class User {
    public string Name { get; init; }
    public int UserId { get; init; }
    public abstract override string ToString();
    public static string GetUserTypes() {
        return "MEMBER";
    }
}