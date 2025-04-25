using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client;

internal class AccountManager
{
    public static AccountManager instance;

    private string? accessToken = null;

    public AccountManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else return;
    }

    public async void DoGitHubAuth()
    {
        if (accessToken != null)
            return;
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
        accessToken = result.AccessToken;
    }
}