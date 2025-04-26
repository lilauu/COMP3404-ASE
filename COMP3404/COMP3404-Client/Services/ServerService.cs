using COMP3404_Shared.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace COMP3404_Client.Services;

public class ServerService
{
    const string ServerURI = "http://localhost:5093";

    private HttpClient m_httpClient;

    private string? m_accessToken = null;

    private UserInfo? m_userInfo = null;

    public string FirstName => m_userInfo?.FirstName ?? "";

    public ServerService(HttpClient httpClient)
    {
         m_httpClient = httpClient;
    }

    public bool IsAuthenticated() => m_accessToken != null;

    public async Task<bool> Authenticate()
    {
        // github registration flow: (OAuth)
        // https://docs.github.com/en/apps/oauth-apps/building-oauth-apps/authorizing-oauth-apps#web-application-flow
        // 1. request identity
        // 2. redirect back to app
        // 3. exchange code for access token
        // 4. use token to access API
        Uri authUri = new("https://github.com/login/oauth/authorize?client_id=Ov23li6gKzCpMMxUThEE");
        Uri redirectUri = new("comp3404://login/github");
#if WINDOWS
        var result = await WinUIEx.WebAuthenticator.AuthenticateAsync(authUri, redirectUri);
#else
        var result = await WebAuthenticator.AuthenticateAsync(authUri, redirectUri);
#endif

        m_accessToken = result.AccessToken;

        return m_accessToken is not null;
    }

    public async Task<UserInfo?> GetUserInfo()
    {
        var res = await MakeAuthenticatedRequest(HttpMethod.Get, new HttpRequestOptions(), "account");
        if (res.StatusCode != System.Net.HttpStatusCode.OK)
        {
            m_userInfo = null;
        }
        else
        {
            string content = await res.Content.ReadAsStringAsync();
            m_userInfo = JsonSerializer.Deserialize<UserInfo>(content);
        }

        return m_userInfo;
    }

    public async Task<HttpResponseMessage> MakeAuthenticatedRequest(HttpMethod method, HttpRequestOptions options, string endpoint, HttpContent? content = null)
    {
        // if not authed, dont attempt to make authenticated request
        if (m_accessToken == null)
            return new();

        HttpRequestMessage message = new()
        {
            Method = method,
            Content = content,
        };
        // force the authentication header
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", m_accessToken);
        // force the request URI
        message.RequestUri = new($"{ServerURI}/{endpoint}");


        return await m_httpClient.SendAsync(message);
    }
}
