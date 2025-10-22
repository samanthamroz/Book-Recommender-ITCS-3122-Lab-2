using System.Dynamic;

namespace Lab2;

public class RatingMapRepository : IRatingMapRepository {
    IRatingMap _ratingMap;
    public RatingMapRepository(IRatingMap ratings) {
        _ratingMap = ratings;
    }
    public int GetUsersRatingOfItem(int userId, int itemId) {
        return _ratingMap.GetItemRating(userId, itemId);
    }
    public void SetUsersRatingOfItem(int userId, int itemId, int rating) {
        _ratingMap.SetItemRating(userId, itemId, rating);
    }
    public List<int> GetUsersRatings(int userId) {
        var ratingDict = _ratingMap.GetUsersRatings(userId);
        List<int> ratings = new();
        foreach (var kvp in ratingDict) {
            ratings.Add(kvp.Value);
        }
        return ratings;
    }
}