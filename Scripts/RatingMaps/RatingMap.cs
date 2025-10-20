namespace Lab2;

public class RatingMap<User, Item> : IRatingMap {
    //Member id, (book id, rating)
    Dictionary<int, Dictionary<int, int>> Ratings = new();

    //Kvps must be in the format (memberId, [rating1, rating2...])|
    //Ratings must be in order starting with book id 0
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
}