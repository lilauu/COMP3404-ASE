using COMP3404_Shared.Models.Accounts;

namespace COMP3404_Server.Repositories;

public interface IUserAccountRepository
{
    public UserAccount? GetById(int id);
    public UserAccount? GetByToken(string token);
    public UserAccount? Add(UserAccount newAccount);

    public void Save();
}
