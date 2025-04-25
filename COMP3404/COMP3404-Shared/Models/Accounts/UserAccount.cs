using COMP3404_Shared.Models.Chats;

namespace COMP3404_Shared.Models.Accounts;

/// <summary>
/// An user account's login and session info.
/// </summary>
public class UserAccount
{
    /// <summary>
    /// The Id of the user account. (from Github)
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// The user's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// A Github access token used to authenticate with Github.
    /// </summary>
    public string GithubToken { get; set; }

    public List<Chat> Chats { get; set; }

    /// <summary>
    /// A token representing the current user's session, allowing repeated requests without the need to reauthenticate
    /// </summary>
    //public string SessionToken { get; set; }
    /// <summary>
    /// The date and time that the <see cref="SessionToken"/> expires.
    /// </summary>
    //public DateTime TokenExpiry { get; set; }
}
