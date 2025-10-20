namespace Lab2;

public class MemberDatabase : IUserDatabase<Member> {
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

    public void SetUser(Member user) {
        try {
            Member doesIdExist = GetUserById(user.UserId);
            members.Remove(doesIdExist); 
        } catch (KeyNotFoundException) {
            //
        } finally {
            members.Add(user);
        }
    }

    public Member GetUserById(int id) {
        throw new KeyNotFoundException();
    }

    public Member GetUserByName(string name) {
        return members.First(m => m.Name == name);
    }

    public int GetCount() {
        return members.Count;
    }

    public int GetNextAvailableId() {
        return members[^1].UserId + 1;
    }
}