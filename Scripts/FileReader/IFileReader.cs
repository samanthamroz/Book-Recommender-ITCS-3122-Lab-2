namespace Lab2;

public interface IFileReader {
    public string[] GetItemFileParsedText(string fileName);

    public List<KeyValuePair<string, int[]>> GetUserFileParsedText(string fileName);
}