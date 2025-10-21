namespace Lab2;

public class Member : User {
    public Member(int userId, string name) {
        UserId = userId;
        Name = name;
    }
    public override string ToString() {
        return $"{Name} (Member ID: {UserId})";
    }
}