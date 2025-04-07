namespace COMP3404_Server.Models;

/// <summary>
/// An user account's login and session info.
/// </summary>
public class AccountAuth
{
    /// <summary>
    /// The Id of the user account.
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// The user's linked Github account's Id.
    /// </summary>
    public int GithubId { get; set; }
    /// <summary>
    /// A Github access token used to authenticate with Github.
    /// </summary>
    public string? GithubToken { get; set; }

    /// <summary>
    /// A token representing the current user's session, allowing repeated requests without the need to reauthenticate
    /// </summary>
    public string? SessionToken { get; set; }
    /// <summary>
    /// The date and time that the <see cref="SessionToken"/> expires.
    /// </summary>
    public DateTime TokenExpiry { get; set; }
}
