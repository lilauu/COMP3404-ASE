using COMP3404_Shared.Models.Api;
using System.Net.Http.Headers;
using System.Text.Json;

namespace COMP3404_Client.Services;

/// <summary>
/// Service for interacting with the COMP3404 API server.
/// </summary>
public class ServerService
{
    const string ServerURI = "http://localhost:5093";

    private HttpClient m_httpClient;

    private string? m_accessToken = null;

    private UserInfo? m_userInfo = null;

    /// <summary>
    /// Gets the user's first name, if current user info is known.
    /// </summary>
    public string GetFirstName() => m_userInfo?.FirstName ?? "";

    /// <summary>
    /// Gets if the current session is authenticated with the COMP3404 API server
    /// </summary>
    public bool IsAuthenticated() => m_accessToken != null;

    /// <summary>
    /// Constructor for the <see cref="ServerService"/>. Typically uses Dependency Injection to resolve the required services.
    /// </summary>
    /// <param name="httpClient">An <see cref="HttpClient"/> that is used to make API requests.</param>
    public ServerService(HttpClient httpClient)
    {
         m_httpClient = httpClient;
    }

    /// <summary>
    /// Begin the authentication flow with the COMP3404 API server, using Github's OAuth.
    /// </summary>
    /// <returns>A Task that returns whether the authentication was successful</returns>
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

    /// <summary>
    /// Get's the authenticated user's profile information from the COMP3404 API server.
    /// </summary>
    /// <returns>A Task that returns a <see cref="UserInfo"/> containing information on the user, or null</returns>
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

    /// <summary>
    /// Make a request to the COMP3404 API server, this function will handle authorisation headers and resolving the URI.
    /// </summary>
    /// <param name="method">The chosen <see cref="HttpMethod"/></param>
    /// <param name="options">Any <see cref="HttpRequestOptions"/> used in the request</param>
    /// <param name="endpoint">The API endpoint to query, should not be prefixed with `/`</param>
    /// <param name="content">Optional body content for the request</param>
    /// <returns></returns>
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
