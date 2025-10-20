namespace Lab2;

public abstract class User {
    public string Name;
    public int UserId;
    public static string GetUserTypes() {
        return "MEMBER";
    }
}