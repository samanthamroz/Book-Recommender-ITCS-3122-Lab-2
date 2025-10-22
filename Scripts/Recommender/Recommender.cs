namespace Lab2;

public class Recommender : IRecommender {
    private IRatingMapRepository _ratingMapRepository;
    private IUserRepository _userRepository;
    private IItemRepository _itemRepository;
    public Recommender(RepositoryCollection repositoryCollection) {
        _ratingMapRepository = repositoryCollection.RatingMapRepository;
        _userRepository = repositoryCollection.UserRepository;
        _itemRepository = repositoryCollection.ItemRepository;
    } 
    
    public void DisplayGlobalRecommendations(int memberId) {
        //foreach other member
        //member difference score = difference between that member's rating and this member's rating
        //remember top 5 lowest user scores
        //for each of those members, pick their highest rating books
        List<int> myRatings = _ratingMapRepository.GetUsersRatings(memberId);

        SortedSet<(int similarity, int userId)> topSimilarUsers = GetTopSimilarUsers(memberId, myRatings);
        List<int> similarUserIds = new();
        foreach (var entry in topSimilarUsers) {
            similarUserIds.Add(entry.userId);
        }

        List<int> topBookIds = GetUsersBooksOrdered(memberId, similarUserIds);
        Console.WriteLine("Here are the items that we think you'll be most likely to enjoy!\n----------");
        for (int i = 0; i < 5; i++) {
            Item item = _itemRepository.GetItem(topBookIds[i]);
            Console.WriteLine($"{i + 1}. {item}");
        }
        Console.WriteLine("------------");
    }

    public void DisplaySingleSimilarUserRecommendations(int memberId) {
        List<int> myRatings = _ratingMapRepository.GetUsersRatings(memberId);

        SortedSet<(int similarity, int userId)> topSimilarUsers = GetTopSimilarUsers(memberId, myRatings);
        

        int topUserId = -1;
        //Get the top user with 5 books to recommend
        foreach(var user in topSimilarUsers) {
            List<int> userIdAsList = new List<int>{user.userId};
            if (GetUsersBooksOrdered(memberId, userIdAsList).Count >= 5) {
                topUserId = user.userId;
            }
        }

        List<int> topUserIdAsList = new List<int>{topUserId};
        List<int> topBookIds = GetUsersBooksOrdered(memberId, topUserIdAsList);

        Console.WriteLine($"You have similar taste in books as {_userRepository.GetUser(topSimilarUsers.Min.userId)}");
        Console.WriteLine("Here are the items they enjoyed most!\n----------");
        for (int i = 0; i < 5; i++) {
            Item item = _itemRepository.GetItem(topBookIds[i]);
            Console.WriteLine($"{i + 1}. {item}");
        }
        Console.WriteLine("------------");
    }

    private SortedSet<(int similarity, int userId)> GetTopSimilarUsers(int memberId, List<int> myRatings) {
        SortedSet<(int similarity, int userId)> topSimilarUsers = new();

        List<int> otherRatings;
        for (int i = 0; i < _userRepository.GetNextAvailableUserId(); i++) {
            if (i == memberId) {
                continue; //skip self
            }

            User otherUser;
            try {
                otherUser = _userRepository.GetUser(i);
                otherRatings = _ratingMapRepository.GetUsersRatings(otherUser.UserId);
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

        int similarity = 0;
        int booksCompared = 0;

        for (int i = 0; i < myRatings.Count; i++) {
            if (myRatings[i] == 0 || otherRatings[i] == 0) {
                continue;
            }
            booksCompared++;
            
            int rawDifference = Math.Abs(myRatings[i] - otherRatings[i]);
            similarity += rawDifference;
        }

        if (booksCompared < 5) {
            return 10000; //not enough books to make a recommendation
        }

        similarity *= 100; //scale
        similarity /= booksCompared; //similarity should scale for the amount of books we looked at

        return similarity;
    }

    private List<int> GetUsersBooksOrdered(int memberId, List<int> similarUserIds) {
        Dictionary<int, int> itemRatingSums = new(); // itemId, sum of ratings

        foreach (int id in similarUserIds) {
            for (int i = 0; i < _itemRepository.GetNextAvailableItemId(); i++) {
                Item item;
                try {
                    item = _itemRepository.GetItem(i);
                    if (_ratingMapRepository.GetUsersRatingOfItem(memberId, i) != 0) {
                        continue; //user has already read this book
                    }

                    int rating = _ratingMapRepository.GetUsersRatingOfItem(id, i);

                    if (rating <= 0) {
                        continue; //similar user didn't like the book
                    }

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

        return rating;// * Math.Abs(rating);
    }
}