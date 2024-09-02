using System.Text.Json;

public class FileHandler : IFileHandler
{
    string path;

    public FileHandler(string path)
    {
        this.path = path;
    }

    public JsonElement GetJSONRoot()
    {
        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        using JsonDocument doc = JsonDocument.Parse(fs);
        JsonElement root = doc.RootElement.Clone();

        return root;
    }

}