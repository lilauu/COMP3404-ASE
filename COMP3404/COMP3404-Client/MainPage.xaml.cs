using System.Net.Http;

namespace COMP3404_Client;

public partial class MainPage : ContentPage
{
    private HttpClient m_HttpClient;

    public MainPage()
    {
        InitializeComponent();
        m_HttpClient = new HttpClient();
    }

    private async void ProfileButton_Clicked(object sender, EventArgs e)
    {
        // temp: start login flow 
        await Task.Run(Thing);
    }

    // github registration flow: (OAuth)
    // https://docs.github.com/en/apps/oauth-apps/building-oauth-apps/authorizing-oauth-apps#web-application-flow
    // 1. request identity
    // 2. redirect back to app
    // 3. exchange code for access token
    // 4. use token to access API
    // notes:
    // WebAuthenticator seems great, but doesnt work on windows. will need windows-specific code i think
    private async void Thing()
    {
        //await Browser.OpenAsync("https://github.com/login/oauth/authorize?client_id=Ov23li6gKzCpMMxUThEE");
        WebAuthenticatorResult authResult = await WebAuthenticator.AuthenticateAsync(
        new Uri("https://github.com/login/oauth/authorize?client_id=Ov23li6gKzCpMMxUThEE"),
        new Uri("comp3404://login/github"));
        //string accessToken = authResult?.AccessToken;

        // Do something with the token
        //Debug.WriteLine(accessToken);

/*        var authorizationRequest = $"https://github.com/login/oauth/authorize?client_id=Ov23li6gKzCpMMxUThEE&redirect_uri=comp3404://login/github";
        // Open the authorization URL in a web browser
        //await Browser.OpenAsync(authorizationRequest);
        WebView view = new()
        {
            Source = authorizationRequest,
        };*/
        
        // Assume we get the auth response with code here, you might need a web view to capture the callback
        //var authCode = await ProcessAuthorizationCode(); // Implement this method

        // Exchange the authorization code for tokens
        /*        var tokenResponse = await ExchangeCodeForToken(authCode);
                if (tokenResponse.IsError)
                {
                    await DisplayAlert("Error", tokenResponse.Error, "OK");
                    return;
                }
                // Use access token to make authenticated API calls
                var userInfo = await GetUserInfo(tokenResponse.AccessToken);*/
    }
    /*private async Task<TokenResponse> ExchangeCodeForToken(string authorizationCode)
    {
        var tokenRequest = new TokenRequest
        {
            Address = App.TokenEndpoint,
            GrantType = "authorization_code",
            ClientId = App.ClientId,
            ClientSecret = App.ClientSecret,
            Code = authorizationCode,
            RedirectUri = App.RedirectUri,
        };
        // Make the token request
        var response = await _httpClient.RequestTokenAsync(tokenRequest);
        return response;
    }
    private async Task<string> GetUserInfo(string accessToken)
    {
        _httpClient.SetBearerToken(accessToken);
        var response = await _httpClient.GetAsync("https://your.api/userinfo");
        return await response.Content.ReadAsStringAsync();
    }*/
}
