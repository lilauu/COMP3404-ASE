using COMP3404_Server.Repositories;
using COMP3404_Shared.Models.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COMP3404_Server.Controllers;

[ApiController]
[Route("chat")]
public class ChatController : ControllerBase
{
    private readonly IUserAccountRepository m_accountRepository;
    private readonly IChatRepository m_chatRepository;

    public ChatController(IUserAccountRepository accountRepository, IChatRepository chatRepository)
    {
        m_accountRepository = accountRepository;
        m_chatRepository = chatRepository;
    }

    /// <summary>
    /// Gets all chats for the user.
    /// Note: this is insecure because rolling my own authentication/authorisation is a lot of effort,
    /// and the brief states "Allow users to choose to store dialogues online (Eg: NoSQL/SQL database or similar)"
    /// which has no mention of it being stored in a secure manner. The login has to be secure, but the data doesn't.
    /// </summary>
    [HttpGet]
    [Route("all")]
    public ActionResult<IEnumerable<Chat>> GetAllChats()
    {
        if (!Utils.GetUserTokenFromHeaders(Request.Headers, out string accessToken))
            return Unauthorized();

        var account = m_accountRepository.GetByToken(accessToken);
        if (account is null)
            return Forbid();

        var chats = m_chatRepository.GetChats(account.AccountId);
        if (chats is null)
            return Ok(new List<Chat>());

        return Ok(chats);
    }

    [HttpPost]
    public ActionResult CreateChat(string chatName, List<ChatMessage> messages)
    {
        if (!Utils.GetUserTokenFromHeaders(Request.Headers, out string accessToken))
            return Unauthorized();

        var account = m_accountRepository.GetByToken(accessToken);
        if (account is null)
            return Forbid();

        var result = m_chatRepository.AddChat(account.AccountId, chatName, messages);
        if (result is null)
            return Problem();

        m_accountRepository.Save();

        return Ok();
    }

}
