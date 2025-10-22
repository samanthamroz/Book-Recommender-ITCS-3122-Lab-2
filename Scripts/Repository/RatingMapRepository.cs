using System.Dynamic;

namespace Lab2;

public class RatingMapRepository : IRatingMapRepository {
    IRatingMap _ratingMap;
    public RatingMapRepository(IRatingMap ratings) {
        _ratingMap = ratings;
    }
    public void ResetAllRatingsOfItem(int itemId) {
        _ratingMap.ResetAllRatingsOfItem(itemId);
    }
    public int GetUsersRatingOfItem(int userId, int itemId) {
        return _ratingMap.GetItemRating(userId, itemId);
    }
    public void SetUsersRatingOfItem(int userId, int itemId, int rating) {
        _ratingMap.SetItemRating(userId, itemId, rating);
    }
    public IReadOnlyDictionary<int, int> GetUsersRatings(int userId) {
        return _ratingMap.GetUsersRatings(userId);
    }
}