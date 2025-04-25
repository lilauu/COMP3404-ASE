using COMP3404_Server.Database;
using COMP3404_Shared.Models.Accounts;
using COMP3404_Shared.Models.Chats;

namespace COMP3404_Server.Repositories;

public class Repository : IUserAccountRepository, IChatRepository
{
    private DatabaseContext m_dbContext;

    public Repository(DatabaseContext dbContext)
    {
        m_dbContext = dbContext;
    }

    UserAccount? IUserAccountRepository.GetById(int id)
    {
        return m_dbContext.Accounts.FirstOrDefault(a => a.AccountId == id);
    }

    UserAccount? IUserAccountRepository.GetByToken(string token)
    {
        return m_dbContext.Accounts.FirstOrDefault(a => a.GithubToken == token);
    }

    UserAccount? IUserAccountRepository.Add(UserAccount newAccount)
    {
        return m_dbContext.Accounts.Add(newAccount).Entity;
    }

    IEnumerable<Chat> IChatRepository.GetChats(int userId)
    {
        return m_dbContext.Chats.Where(c => c.OwnerId == userId);
    }

    Chat? IChatRepository.AddChat(int userId, string chatName, IEnumerable<ChatMessage> messages)
    {
        Chat newChat = new()
        {
            OwnerId = userId,
            ChatName = chatName,
            Messages = new(messages)
        };
        return m_dbContext.Chats.Add(newChat).Entity;
    }
}
