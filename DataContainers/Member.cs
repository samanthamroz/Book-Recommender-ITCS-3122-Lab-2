namespace Lab2;

public class Member : User {
    public string Name { get; set; }
    public Member(int userId, string name) {
        UserId = userId;
        Name = name;
    }
}