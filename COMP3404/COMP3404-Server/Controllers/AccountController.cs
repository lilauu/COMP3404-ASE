using COMP3404_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web;

namespace COMP3404_Server.Controllers;

[ApiController]
[Route("account/")]
public class AccountController : ControllerBase
{
    private string m_clientSecret = "";

    public AccountController()
    {
        // read client secret from file
        StreamReader sr = new("../config.txt");
        m_clientSecret = sr.ReadLine() ?? throw new Exception("Couldn't read first line of config.txt?");
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

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["client_id"] = "Ov23li6gKzCpMMxUThEE";
        query["client_secret"] = m_clientSecret;
        query["code"] = code;
        string queryString = query.ToString() ?? throw new Exception("What");
        message.RequestUri = new($"https://github.com/login/oauth/access_token?{queryString}");

        var response = client.Send(message);
        // todo: handle errors?
        var parsedResponse = JsonSerializer.Deserialize<GithubOAuthResponse>(response.Content.ReadAsStringAsync().Result);

        // todo: if successful, create an account or login to the account, generating a session token and redirecting the user
        // check if account already exists, if so, regen the session token, update name etc.
        // if it doesn't exist, create one, with a new session token, etc.

        return parsedResponse?.TokenType ?? "Failed?";
    }
}
