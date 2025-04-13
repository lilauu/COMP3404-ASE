using COMP3404_Server.Database;
using COMP3404_Shared.Models.Accounts;

namespace COMP3404_Server.Repositories;

public class Repository : IUserAccountRepository
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

    UserAccount? IUserAccountRepository.Add(UserAccount newAccount)
    {
        return m_dbContext.Accounts.Add(newAccount).Entity;
    }
}
