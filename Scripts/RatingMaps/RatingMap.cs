namespace Lab2;

public class RatingMap : IRatingMap {
    //Member id, (book id, rating)
    Dictionary<int, Dictionary<int, int>> Ratings = new();


    //Kvps must be in the format (memberId, [rating1, rating2...])|
    //Ratings must be in order starting with book id 0
    //This assumes ratings were correctly input (only odd values between -5 and 5 or 0)
    public RatingMap(List<KeyValuePair<int, int[]>> kvps, IItemDatabase itemDatabase) {
        int itemsInDatabase = itemDatabase.GetCount();

        foreach (var kvp in kvps) {
            int memberId = kvp.Key;
            int[] memberRatings = kvp.Value;

            Ratings.TryAdd(memberId, new());

            for (int itemId = 0; itemId < memberRatings.Length && itemId < itemsInDatabase; itemId++) {
                int rating;
                try {
                    rating = memberRatings[itemId];
                } catch (IndexOutOfRangeException) {
                    rating = 0; //ratings file didn't contain this member's rating for this book
                }

                Ratings[memberId].TryAdd(itemId, rating);
            }
        }
    }

    public void SetItemRating(int memberId, int itemId, int rating) {
        bool isValid = rating == -5 || rating == -3 || rating == 0 || rating == 1 || rating == 3 || rating == 5;
        if (!isValid) {
            throw new ArgumentException("Rating can only be -5, -3, 0, 1, 3, or 5");
        }

        foreach (var outerKvp in Ratings) {
            if (outerKvp.Key == memberId) {
                var innerKvp = outerKvp.Value;
                if (!innerKvp.TryAdd(itemId, rating)) {
                    //have to update an item they've already rated
                    innerKvp[itemId] = rating;
                }
            }
        }
    }

    public int GetItemRating(int memberId, int itemId) {
        if (Ratings.TryGetValue(memberId, out var memberRatings)) {
            if (memberRatings.TryGetValue(itemId, out var rating)) {
                return rating;
            }
            //happens if member hasn't rated the item
            SetItemRating(memberId, itemId, 0);
            return 0;
        }
        //happens if the member doesn't exist
        SetItemRating(memberId, itemId, 0);
        return 0;
    }
}