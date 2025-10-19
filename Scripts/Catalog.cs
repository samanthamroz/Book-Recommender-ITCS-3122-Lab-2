using System.Data;

namespace Lab2;

public interface ICatalog {
    public void AddUser();
    public void AddItem();
    public void AddItemRating(int userId);
    public void DisplayUsersRatings(int userId);
}

public class Catalog : ICatalog {
    public Catalog() {
        
    }

    public void AddUser() {
        

        IUserDatabase database = Repository.Instance.GetUserDatabaseOfType("Member");
        int id = database.GetNextAvailableId();
        string name;
        
        Member newMember = new(id, name);
        database.SetUser(newMember);
    }

    public void AddItem() {
        
    }

    public void AddItemRating(int userId) {
        
    }

    public void DisplayUsersRatings(int userId) {
        
    }
}