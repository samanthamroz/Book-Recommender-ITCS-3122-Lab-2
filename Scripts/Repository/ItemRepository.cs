namespace Lab2;

public class ItemRepository : IItemRepository {
    IItemDatabase _itemDatabase;
    public ItemRepository(IItemDatabase items) {
        _itemDatabase = items;
    }

    public int GetNextAvailableItemId() {
        try {
            return _itemDatabase.GetNextAvailableId();
        } catch (IndexOutOfRangeException) {
            return 0;
        }
    }

    public int GetItemCount() {
        return _itemDatabase.GetCount();
    }

    public void AddItem(Item item) {
        _itemDatabase.SetItem(item);
    }

    public Item GetItem(int itemId) {
        return _itemDatabase.GetItemById(itemId);
    }
}