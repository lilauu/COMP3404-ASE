using System.Net.Http;

namespace COMP3404_Client;

#if WINDOWS
using WebAuthenticator = WinUIEx.WebAuthenticator;
#endif

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

    }
}
