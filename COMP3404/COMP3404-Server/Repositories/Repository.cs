using COMP3404_Server.Database;
using COMP3404_Shared.Models.Accounts;
using COMP3404_Shared.Models.Chats;
using Microsoft.EntityFrameworkCore;

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
        return m_dbContext.Accounts.FirstOrDefault(a => a.GithubAccountId == id);
    }

    UserAccount? IUserAccountRepository.GetByToken(string token)
    {
        return m_dbContext.Accounts.FirstOrDefault(a => a.GithubToken == token);
    }

    UserAccount? IUserAccountRepository.Add(UserAccount newAccount)
    {
        var ent = m_dbContext.Accounts.Add(newAccount).Entity;
        Save();
        return ent;
    }

    IEnumerable<Chat> IChatRepository.GetChats(int userId)
    {
        return m_dbContext.Chats.Where(c => c.OwnerId == userId).Include(c => c.Messages);
    }

    Chat? IChatRepository.AddChat(int userId, string chatName, IEnumerable<ChatMessage> messages)
    {
        using (m_dbContext.Database.BeginTransaction())
        {
            var messageEntities = new List<ChatMessage>();
            foreach (var message in messages)
            {
                messageEntities.Add(m_dbContext.ChatMessages.Add(message).Entity);
            }
            Chat newChat = new()
            {
                OwnerId = userId,
                ChatName = chatName,
                Messages = new(messageEntities),
            };
            var ent = m_dbContext.Chats.Add(newChat).Entity;
            m_dbContext.Database.CommitTransaction();
            Save();
            return ent;
        }
    }

    public void Save()
    {
        m_dbContext.SaveChanges();
    }
}
