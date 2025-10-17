namespace Lab2;

public class MemberBookRatingsDatabase : IRatingMap {
    List<List<int>> Ratings = new();
    public MemberBookRatingsDatabase(List<KeyValuePair<string, int[]>> kvps) {
        for (int i = 0; i < kvps.Count; i++) {
            for (int j = 0; j < kvps[i].Value.Length; j++) {
                Ratings[i][j] = kvps[i].Value[j];
            }
        }
    }
}