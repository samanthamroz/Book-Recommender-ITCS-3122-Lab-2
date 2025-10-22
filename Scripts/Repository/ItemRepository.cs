namespace Lab2;

public class ItemRepository : IItemRepository {
    IItemDatabase _itemDatabase;
    public ItemRepository(IItemDatabase items) {
        _itemDatabase = items;
    }

    public int GetNextAvailableItemId() {
        return _itemDatabase.GetNextAvailableId();
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