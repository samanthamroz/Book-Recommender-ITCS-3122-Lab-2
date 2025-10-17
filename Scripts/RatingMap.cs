namespace Lab2;

public class RatingMap: IRatingMap {
    //Member id, (book id, rating)
    Dictionary<int, Dictionary<int, int>> Ratings = new();

    //Kvps must be in the format (memberId, [rating1, rating2...])|
    //Ratings must be in order starting with book id 0
    public RatingMap(List<KeyValuePair<int, int[]>> kvps, IItemDatabase itemDatabase) {
        int itemsInDatabase = itemDatabase.GetItemCount();

        foreach (var kvp in kvps) {
            int memberId = kvp.Key;
            int[] memberRatings = kvp.Value;

            if (!Ratings.ContainsKey(memberId)) {
                Ratings.Add(memberId, new());
            }

            for (int itemId = 0; itemId < memberRatings.Length && itemId < itemsInDatabase; itemId++) {
                int rating = memberRatings[itemId];
                
                Ratings[memberId].Add(itemId, rating);
            }
        }
    }
}