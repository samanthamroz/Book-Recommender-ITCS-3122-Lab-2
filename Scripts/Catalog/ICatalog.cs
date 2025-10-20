namespace Lab2;

public interface ICatalog {
    void AddUser();
    User GetUserById(string type, int id);
    void AddItem();
    void AddItemRating(int userId);
    void DisplayUsersRatings(int userId);
}