namespace Lab2;

public class Recommender : IRecommender {
    public Recommender() {
        
    } 
    
    public void DisplayGlobalRecommendations(int memberId) {
        //foreach other member
        //member difference score = difference between that member's rating and this member's rating
        //remember top 5 lowest user scores
        //for each of those members, pick their highest rating books
        IRepository repo = Repository.Instance;
        List<int> myRatings = repo.GetUsersRatings(memberId);

        SortedSet<(int similarity, int userId)> topSimilarUsers = GetTopSimilarUsers(memberId, myRatings);
        List<int> similarUserIds = new();
        foreach (var entry in topSimilarUsers) {
            similarUserIds.Add(entry.userId);
        }

        List<int> topBookIds = GetUsersBooksOrdered(memberId, similarUserIds);
        Console.WriteLine("Here are the items that we think you'll be most likely to enjoy!\n----------");
        for (int i = 0; i < 5; i++) {
            Item item = repo.GetItem(topBookIds[i]);
            Console.WriteLine($"{i}. {item}");
        }
        Console.WriteLine("------------");
    }

    public void DisplaySingleSimilarUserRecommendations(int memberId) {
        IRepository repo = Repository.Instance;
        List<int> myRatings = repo.GetUsersRatings(memberId);

        SortedSet<(int similarity, int userId)> topSimilarUsers = GetTopSimilarUsers(memberId, myRatings);
        List<int> topUsersBooks = new List<int>{topSimilarUsers.Min.userId};
        List<int> topBookIds = GetUsersBooksOrdered(memberId, topUsersBooks);

        Console.WriteLine($"You have similar taste in books as {repo.GetUser(topSimilarUsers.Min.userId)}");
        Console.WriteLine("Here are the items they enjoyed most!\n----------");
        for (int i = 0; i < 5; i++) {
            Item item = repo.GetItem(topBookIds[i]);
            Console.WriteLine($"{i}. {item}");
        }
        Console.WriteLine("------------");
    }

    private SortedSet<(int similarity, int userId)> GetTopSimilarUsers(int memberId, List<int> myRatings) {
        SortedSet<(int similarity, int userId)> topSimilarUsers = new();

        IRepository repo = Repository.Instance;

        List<int> otherRatings;
        for (int i = 0; i < repo.GetNextAvailableUserId(); i++) {
            if (i == memberId) {
                continue; //skip self
            }

            User otherUser;
            try {
                otherUser = repo.GetUser(i);
                otherRatings = repo.GetUsersRatings(otherUser.UserId);
            } catch (KeyNotFoundException) {
                //ID didn't have a user associated with it
                continue;
            }

            int similarity = GetSimilarity(myRatings, otherRatings);

            if (topSimilarUsers.Count < 5) {
                topSimilarUsers.Add((similarity, otherUser.UserId));
            } else {
                // Get the worst (highest similarity score) in our top 5
                var maxEntry = topSimilarUsers.Max;
                
                if (similarity < maxEntry.similarity) {
                    topSimilarUsers.Remove(maxEntry);
                    topSimilarUsers.Add((similarity, otherUser.UserId));
                }
            }
        }

        return topSimilarUsers;
    }

    private int GetSimilarity(List<int> myRatings, List<int> otherRatings) {
        if (myRatings.Count != otherRatings.Count) {
            throw new Exception("My ratings must have the same length as other ratings");
        }

        int similarity = 0; //0 is perfect similarity

        for (int i = 0; i < myRatings.Count; i++) {
            if (myRatings[i] == 0 || otherRatings[i] == 0) {
                //one of them hasn't rated the book, so don't factor it in
                continue;
            }
            int rawDifference = Math.Abs(myRatings[i] - otherRatings[i]);
            int exponent = rawDifference / 2;
            similarity += (int)Math.Pow(2, exponent);
        }

        return similarity;
    }

    private List<int> GetUsersBooksOrdered(int memberId, List<int> similarUserIds) {
        Dictionary<int, int> itemRatingSums = new(); // itemId, sum of ratings
        
        IRepository repo = Repository.Instance;

        foreach (int id in similarUserIds) {
            for (int i = 0; i < repo.GetNextAvailableItemId(); i++) {
                Item item;
                try {
                    item = repo.GetItem(i);
                    if (repo.GetUsersRatingOfItem(memberId, i) != 0) {
                        continue; //user has already read this book
                    }

                    int rating = repo.GetUsersRatingOfItem(id, i);
                    int weightedRating = GetWeightedRating(rating);

                    if (!itemRatingSums.TryAdd(i, weightedRating)) {
                        itemRatingSums[i] += weightedRating;
                    }
                } catch (KeyNotFoundException) {
                    //item didn't exist or user hasn't rated it
                    continue;
                }
            }
        }
        
        var sorted = itemRatingSums.OrderByDescending(kvp => kvp.Value);
        return sorted.Select(kvp => kvp.Key).ToList(); //return just the book ids
    }

    private int GetWeightedRating(int rating) {
        if (rating < 0) {
            return -100;
        } 

        return (int)Math.Pow(rating, 2);
    }
}