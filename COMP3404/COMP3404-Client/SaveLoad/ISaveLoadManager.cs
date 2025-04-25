using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.SaveLoad;

public interface ISaveLoadManager
{
    void SaveChat(Chat chat);
    Task<IEnumerable<Chat>> LoadChatsAsync();
}
