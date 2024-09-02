using System.Text.Json;

public interface IFileHandler
{
    //Gets JSON root element of file
    JsonElement GetJSONRoot();
}