﻿using COMP3404_Server.Database;
using COMP3404_Server.Models.Api;
using COMP3404_Server.Repositories;
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
    private IUserAccountRepository m_accountRepository;
    private HttpClient m_client;

    private string ClientSecret => Program.Configuration?.GetValue<string>("GITHUB_CLIENT_SECRET") ?? "";

    public AccountController(IUserAccountRepository accountRepository, HttpClient client)
    {
        m_accountRepository = accountRepository;
        m_client = client;
    }

    public string GetGithubToken(string code)
    {
        HttpRequestMessage message = new();
        message.Method = HttpMethod.Post;
        message.Headers.Add("Accept", "application/json");

        // todo: refactor this mess

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["client_id"] = "Ov23li6gKzCpMMxUThEE";
        query["client_secret"] = ClientSecret;
        query["code"] = code;
        string queryString = query.ToString() ?? throw new Exception("What");
        message.RequestUri = new($"https://github.com/login/oauth/access_token?{queryString}");

        var response = m_client.Send(message);
        string stringResponse = response.Content.ReadAsStringAsync().Result;
        var parsedResponse = JsonSerializer.Deserialize<GithubOAuthResponse>(stringResponse)
            ?? throw new Exception($"Failed to parse response from https://github.com/login/oauth/access_token?\n {stringResponse}");
        return parsedResponse.AccessToken;
    }

    /// <summary>
    /// Authenticates with Github, creating an account if one doesn't exist
    /// </summary>
    [HttpGet]
    [Route("login/github")]
    public ActionResult<string> GithubLogin(string code)
    {
        // use the code from the client to auth with github and get account info
        string accessToken = GetGithubToken(code);

        HttpRequestMessage message = new();
        message = new();
        message.Method = HttpMethod.Get;
        message.RequestUri = new("https://api.github.com/user");
        message.Headers.Add("User-Agent", "COMP3404");
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("Authorization", $"Bearer {accessToken}");
        message.Headers.Add("X-GitHub-Api-Version", "2022-11-28");

        var userResponse = m_client.Send(message);
        string stringUserResponse = userResponse.Content.ReadAsStringAsync().Result;
        var parsedUserResponse = JsonSerializer.Deserialize<GetUserResponse>(stringUserResponse);
        if (parsedUserResponse is null)
            return Problem($"Failed to parse response from https://api.github.com/user?\n {stringUserResponse}");

        var foundAccount = m_accountRepository.GetById(parsedUserResponse.Id);
        if (foundAccount is null)
        {
            // create account
            UserAccount newAccount = new()
            {
                AccountId = parsedUserResponse.Id,
                GithubToken = accessToken,
                FirstName = parsedUserResponse.Name,
            };
            foundAccount = m_accountRepository.Add(newAccount) ?? throw new Exception("Failed to create account?");
        }
        else
        {
            // update account info
            foundAccount.FirstName = parsedUserResponse.Name;
            foundAccount.GithubToken = accessToken;
        }

        // todo: create session token for account and return it

        return Ok(foundAccount.FirstName);
    }
}
