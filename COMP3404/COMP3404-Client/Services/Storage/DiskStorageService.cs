using COMP3404_Shared.Models.Chats;
using System.Text.Json;

namespace COMP3404_Client.Services.Storage;

public class DiskStorageService : IStorageService
{
    // To allow DI to work
    public DiskStorageService()
    { }

    public void SaveChat(Chat chat)
    {
        string directoryPath = GetTargetDirectory();
        string filePath = Path.Combine(directoryPath, $"{chat.ChatName}.json");

        DeleteFileIfExists(filePath);

        SaveDataToFile(chat, filePath);
    }

    public async Task<IEnumerable<Chat>> LoadChatsAsync()
    {
        string directoryPath = GetTargetDirectory();
        var files = Directory.GetFiles(directoryPath, "*.json");

        List<Task<Chat>> tasks = [];

        foreach (var file in files)
        {
            // load each chat on a background thread
            tasks.Add(Task.Run(() =>
            {
                Chat chat = LoadDataFromFile<Chat>(file);
                return chat;
            }));
        }

        var results = await Task.WhenAll(tasks);

        return results?.ToList() ?? [];
    }

    private string GetTargetDirectory() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "COMP3404");

    private void SaveDataToFile<T>(T data, string fileName)
    {
        //Create the file path using the MyDocuments folder
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        path = Path.Combine(path, "COMP3404");

        if (!Path.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string filePath = Path.Combine(path, fileName);

        //Write the data into the path
        File.WriteAllText(filePath, JsonSerializer.Serialize(data, options:new() { WriteIndented = true, }));
    }

    private T LoadDataFromFile<T>(string fileName)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        path = Path.Combine(path, "COMP3404");

        string filePath = Path.Combine(path, fileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"{filePath}");

        var text = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(text) ?? throw new Exception("Failed to parse JSON, what the fuck");
    }

    private void DeleteFileIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
