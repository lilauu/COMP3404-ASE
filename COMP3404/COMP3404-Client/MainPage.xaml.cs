namespace COMP3404_Client;

#if WINDOWS
// hack to get around WebAuthenticator not working rn on windows :/
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
		WebAuthenticatorResult authResult;

#pragma warning disable CA1416 // calling stuff on unsupported platform, intellisense isnt very intelli sometimes i think
		authResult = await WebAuthenticator.AuthenticateAsync(
		new Uri("https://github.com/login/oauth/authorize?client_id=Ov23li6gKzCpMMxUThEE"),
		new Uri("comp3404://login/github"));
#pragma warning restore CA1416

		authResult.CallbackUri.ToString();
		// THIS WORKS UP TO HERE
		// todo: change redirect url on github to point to server URL? i think browser should just redirect to server auth endpoint and then server redirects back to app? maybe?
		// alternatively i could just ping the API with the code and such from here that would work

		//string accessToken = authResult.AccessToken;
		//m_nameBox.Text = accessToken;

		// Do something with the token
		//Debug.WriteLine(accessToken);

	}
}
