namespace Lab2;

public class FileReader {
    public static string[] GetItemFileParsedText(string fileName) {
        string[] lines = File.ReadAllLines(fileName);

        List<string> individualParts = new();

        foreach (string line in lines) {
            foreach (string s in line.Split(',')) {
                individualParts.Add(s);
            }
        }

        string[] returnParts = individualParts.ToArray();

        return returnParts;
    }

    public static List<KeyValuePair<string, int[]>> GetUserFileParsedText(string fileName) {
        List<KeyValuePair<string, int[]>> returnList = new();

        string[] lines = File.ReadAllLines(fileName);

        for (int i = 0; i < lines.Length; i += 2) {
            KeyValuePair<string, int[]> kvp;

            string[] stringRatings = lines[i + 1].Split(' ');
            foreach (string s in stringRatings) {
                Console.Write(s);
            }

            int[] ratings = new int[stringRatings.Length];
            for (int j = 0; j < stringRatings.Length; j++)
            {
                if (int.TryParse(stringRatings[j], out ratings[j])) {
                    
                } else {
                    ratings[j] = 0;
                }
            }

            kvp = new(lines[i], ratings);
            returnList.Add(kvp);
        }

        return returnList;
    }
}