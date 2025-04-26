using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.Services.Storage;

public interface IStorageService
{
    void SaveChat(Chat chat);
    Task<IEnumerable<Chat>> LoadChatsAsync();
}
