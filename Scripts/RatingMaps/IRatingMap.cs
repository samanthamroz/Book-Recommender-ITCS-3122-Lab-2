namespace Lab2;

public interface IRatingMap {
    public void SetItemRating(int memberId, int itemId, int rating);
    public int GetItemRating(int memberId, int itemId);
}