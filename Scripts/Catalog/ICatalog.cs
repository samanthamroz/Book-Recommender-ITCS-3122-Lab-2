namespace Lab2;

public interface ICatalog {
    void AddUser();
    void AddItem();
    void AddItemRating(User loggedInUser);
    void DisplayUsersRatings(User loggedInUser);
}