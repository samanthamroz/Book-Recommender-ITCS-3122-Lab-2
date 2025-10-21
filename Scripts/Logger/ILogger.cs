namespace Lab2;

public interface ILogger {
    public int LoggedInId { get; }
    public User? LoggedInUser { get; }
    public void LogIn();
    public void LogOut();
    public bool IsLoggedIn();
}