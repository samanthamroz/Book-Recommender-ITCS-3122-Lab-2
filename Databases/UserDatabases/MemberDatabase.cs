namespace Lab2;

public class MemberDatabase : IUserDatabase {
    List<Member> members = new();

    //This constructor expects the parsed text array to be in the following format:
    //[book1author, book1title, book1year, book2author, book2title, book2year, etc]
    public MemberDatabase(List<KeyValuePair<string, int[]>> kvps) {
        for (int i = 0; i < kvps.Count; i++) {
            Member newMember = new(i, kvps[i].Key);
        }
    }

    public void SetUser(User user) {
        throw new NotImplementedException();
    }

    public User GetUserById(int id) {
        throw new NotImplementedException();
    }
}