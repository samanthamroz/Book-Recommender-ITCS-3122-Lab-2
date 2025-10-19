namespace Lab2;

public interface IRecommender {
    void DisplayRecommendations(int memberId);
}

public class Recommender : IRecommender {
    public Recommender() {
        
    } 
    
    public void DisplayRecommendations(int memberId) {
        throw new NotImplementedException();
    }
}