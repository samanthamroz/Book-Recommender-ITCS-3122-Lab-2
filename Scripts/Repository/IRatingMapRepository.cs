using System.Dynamic;

namespace Lab2;

public interface IRatingMapRepository {
    int GetUsersRatingOfItem(int userId, int itemId);
    void SetUsersRatingOfItem(int userId, int itemId, int rating);
    IReadOnlyDictionary<int, int> GetUsersRatings(int userId);
}