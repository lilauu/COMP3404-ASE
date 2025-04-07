using COMP3404_Server.Database;
using COMP3404_Server.Models.Api;
using COMP3404_Shared.Models.Accounts;
using COMP3404_Shared.Models.Api.Github;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web;

namespace COMP3404_Server.Controllers;

[ApiController]
[Route("account/")]
public class AccountController : ControllerBase
{
    private DatabaseContext m_dbContext;
    private string m_clientSecret = "";

    public AccountController(DatabaseContext dbContext)
    {
        // read client secret from file
        StreamReader sr = new("../config.txt");
        m_clientSecret = sr.ReadLine() ?? throw new Exception("Couldn't read first line of config.txt?");

        m_dbContext = dbContext;
    }

    /// <summary>
    /// Authenticates with Github, creating an account if one doesn't exist
    /// </summary>
    [HttpGet]
    [Route("login/github")]
    public string GithubLogin(string code)
    {
        // use the code from the client to auth with github and get account info
        HttpClient client = new();
        HttpRequestMessage message = new();
        message.Method = HttpMethod.Post;
        message.Headers.Add("Accept", "application/json");

        // todo: refactor this mess

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["client_id"] = "Ov23li6gKzCpMMxUThEE";
        query["client_secret"] = m_clientSecret;
        query["code"] = code;
        string queryString = query.ToString() ?? throw new Exception("What");
        message.RequestUri = new($"https://github.com/login/oauth/access_token?{queryString}");

        var response = client.Send(message);
        string stringResponse = response.Content.ReadAsStringAsync().Result;
        var parsedResponse = JsonSerializer.Deserialize<GithubOAuthResponse>(stringResponse);
        if (parsedResponse is null)
            return $"Failed to parse response from https://github.com/login/oauth/access_token?\n {stringResponse}";

        message = new();
        message.Method = HttpMethod.Get;
        message.RequestUri = new("https://api.github.com/user");
        message.Headers.Add("User-Agent", "COMP3404");
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("Authorization", $"Bearer {parsedResponse.AccessToken}");
        message.Headers.Add("X-GitHub-Api-Version", "2022-11-28");

        var userResponse = client.Send(message);
        string stringUserResponse = userResponse.Content.ReadAsStringAsync().Result;
        var parsedUserResponse = JsonSerializer.Deserialize<GetUserResponse>(stringUserResponse);
        if (parsedUserResponse is null)
            return $"Failed to parse response from https://api.github.com/user?\n {stringUserResponse}";

        var foundAccount = m_dbContext.Accounts.FirstOrDefault(acc => acc.AccountId == parsedUserResponse.Id);
        if (foundAccount is null)
        {
            // create account
            UserAccount newAccount = new()
            {
                AccountId = parsedUserResponse.Id,
                GithubToken = parsedResponse.AccessToken,
                FirstName = parsedUserResponse.Name,
            };
            foundAccount = m_dbContext.Accounts.Add(newAccount).Entity;
        }
        else
        {
            // update account info
            foundAccount.FirstName = parsedUserResponse.Name;
            foundAccount.GithubToken = parsedResponse.AccessToken;
        }

        // todo: create session token for account and return it

        return foundAccount.FirstName;
    }
}
