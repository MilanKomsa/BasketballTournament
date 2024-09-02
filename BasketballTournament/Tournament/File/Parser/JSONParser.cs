using System.Text.Json;

class JSONParser
{
    JsonElement root;

    public JSONParser(IFileHandler handler)
    {
        this.root = handler.GetJSONRoot();
    }

    public JsonElement Root { get => root; }
}