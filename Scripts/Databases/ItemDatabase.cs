namespace Lab2;

public class ItemDatabase : IItemDatabase {
    List<Item> items;

    //This constructor expects the parsed text array to be in the following format:
    //[book1author, book1title, book1year, book2author, book2title, book2year, etc]
    public ItemDatabase(List<Item> initialItems) {
        items = initialItems;
    }

    public int GetCount() {
        return items.Count;
    }

    public int GetNextAvailableId() {
        return items[^1].ItemId + 1;
    }

    public void SetItem(Item item) {
            //shorthand "for each Member m, m.UserId == member.UserId, remove"
        items.RemoveAll(i => i.ItemId == item.ItemId);
        
        items.Add(item);
    }

    public Item GetItemById(int id) {
        foreach (Item i in items) {
            if (i.ItemId == id) {
                return i;
            }
        }
        throw new KeyNotFoundException($"No item found with ID {id}");
    }
}