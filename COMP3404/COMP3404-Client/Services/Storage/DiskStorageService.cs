using COMP3404_Shared.Models.Chats;
using System.Text.Json;

namespace COMP3404_Client.Services.Storage;

/// <summary>
/// Service for saving and loading <see cref="Chat"/>s from disk.
/// </summary>
public class DiskStorageService : IStorageService
{
    /// <summary>
    /// Constructor for <see cref="DiskStorageService"/>
    /// </summary>
    public DiskStorageService()
    { }

    /// <summary>
    /// Saves a <see cref="Chat"/> to disk.
    /// </summary>
    /// <param name="chat">The chat to save.</param>
    public void SaveChat(Chat chat)
    {
        string directoryPath = GetTargetDirectory();
        string filePath = Path.Combine(directoryPath, $"{chat.ChatName}.json");

        DeleteFileIfExists(filePath);

        SaveDataToFile(chat, filePath);
    }

    /// <summary>
    /// Loads all saved chats from disk.
    /// </summary>
    /// <returns>A Task which returns an enumerable of all successfully loaded chats</returns>
    public async Task<IEnumerable<Chat>> LoadChatsAsync()
    {
        string directoryPath = GetTargetDirectory();
        if (!Directory.Exists(directoryPath))
            return [];

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
