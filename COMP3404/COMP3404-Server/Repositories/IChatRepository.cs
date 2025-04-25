using COMP3404_Shared.Models.Chats;

namespace COMP3404_Server.Repositories;

public interface IChatRepository
{
    public IEnumerable<Chat> GetChats(int userId);
    public Chat? AddChat(int userId, string chatName, IEnumerable<ChatMessage> messages);

    public void Save();
}
