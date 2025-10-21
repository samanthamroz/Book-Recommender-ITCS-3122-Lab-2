namespace Lab2;

public interface IRecommender {
    void DisplayGlobalRecommendations(int memberId);
    void DisplaySingleSimilarUserRecommendations(int memberId);
}