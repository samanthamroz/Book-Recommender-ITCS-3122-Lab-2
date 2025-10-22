namespace Lab2;

public interface IItemRepository {
    int GetNextAvailableItemId();
    int GetItemCount();
    void AddItem(Item item);
    Item GetItem(int itemId);
}