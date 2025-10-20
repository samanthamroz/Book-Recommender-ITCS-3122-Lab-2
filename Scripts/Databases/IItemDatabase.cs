namespace Lab2;

public interface IItemDatabase : IDatabase {
    void SetItem(Item item);

    Item GetItemById(int id);
}