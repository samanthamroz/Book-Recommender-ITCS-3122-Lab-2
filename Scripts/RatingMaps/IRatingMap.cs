namespace Lab2;

public interface IRatingMap {
    void ResetAllRatingsOfItem(int itemId);
    void SetItemRating(int memberId, int itemId, int rating);
    int GetItemRating(int memberId, int itemId);
    IReadOnlyDictionary<int, int> GetUsersRatings(int memberId);
}