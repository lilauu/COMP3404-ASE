using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.Services.Storage;

/// <summary>
/// Interface for storing and loading chats.
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Save a <see cref="Chat"/>
    /// </summary>
    /// <param name="chat">The chat to save</param>
    void SaveChat(Chat chat);

    /// <summary>
    /// Loads all saved chats.
    /// </summary>
    /// <returns>A Task that returns an enumerable of all loaded chats.</returns>
    Task<IEnumerable<Chat>> LoadChatsAsync();
}
