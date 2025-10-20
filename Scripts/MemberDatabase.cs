namespace Lab2;

public class MemberDatabase : IUserDatabase {
    List<Member> members = new();

    //This constructor expects the parsed text array to be in the following format:
    //[(memberName, [0, 1, 2, 3... (ratings of all books in order)]), (...,[...])]
    public MemberDatabase(List<KeyValuePair<string, int[]>> kvps) {
        for (int i = 0; i < kvps.Count; i++) {
            Member newMember = new(i, kvps[i].Key);
            members.Add(newMember);
        }
    }

    //Transforms name-based data to ID-based data (for RatingMap)
    public List<KeyValuePair<int, int[]>> MapNamesToIds(List<KeyValuePair<string, int[]>> kvps) {
        List<KeyValuePair<int, int[]>> returnStruct = new();
        
        foreach (var kvp in kvps) {
            int memberId = GetUserByName(kvp.Key).UserId;
            returnStruct.Add(new(memberId, kvp.Value));
        }
        
        return returnStruct;
    }

    public void SetUser(User user) {
        if (user is not Member member) {
            throw new ArgumentException("MemberDatabase can only store Member objects");
        }
            //shorthand "for each Member m, m.UserId == member.UserId, remove"
        members.RemoveAll(m => m.UserId == member.UserId);
        
        members.Add(member);
    }

    public User GetUserById(int id) {
        foreach (Member m in members) {
            if (m.UserId == id) {
                return m;
            }
        }
        throw new KeyNotFoundException($"No member found with ID {id}");
    }

    public User GetUserByName(string name) {
        foreach (Member m in members) {
            if (m.Name == name) {
                return m;
            }
        }
        throw new KeyNotFoundException($"No member found with name {name}");
    }

    public int GetCount() {
        return members.Count;
    }

    public int GetNextAvailableId() {
        return members[^1].UserId + 1;
    }
}