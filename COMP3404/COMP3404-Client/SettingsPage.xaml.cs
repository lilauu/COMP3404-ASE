namespace COMP3404_Client;

public partial class SettingsPage : ContentPage
{

    /*private const string GitHubClientId = "YOUR_GITHUB_CLIENT_ID";
    private const string GitHubClientSecret = "YOUR_GITHUB_CLIENT_SECRET";
    private const string GitHubRedirectUri = "YOUR_REDIRECT_URI";*/

    public SettingsPage()
	{
		InitializeComponent();
	}

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        // shell nav to main page
        await Shell.Current.GoToAsync("///" + nameof(MainPage));
    }

    private async void OnHistoryButtonClicked(object sender, EventArgs e)
    {
        // shell nav to history page
        await Shell.Current.GoToAsync("///" + nameof(HistoryPage));
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        /*try
        {
            // GitHub authorization URL
            var authUrl = $"https://github.com/login/oauth/authorize?client_id={GitHubClientId}&redirect_uri={GitHubRedirectUri}&scope=user";

            // Use WebAuthenticator to start the OAuth flow
            var result = await WebAuthenticator.Default.AuthenticateAsync(new Uri(authUrl), new Uri(GitHubRedirectUri));

            if (result != null)
            {
                // GitHub will redirect to your Redirect URI with the code parameter
                var code = result?.QueryParameters["code"];

                if (code != null)
                {
                    // Exchange the code for an access token
                    var token = await GetGitHubAccessToken(code);
                    // Use the token as needed (e.g., store it or call GitHub APIs)

                    // Hide the GitHub login button after successful login
                    LoginButton.IsVisible = false;
                }
                else
                {
                    await DisplayAlert("Error", "GitHub authentication failed", "OK");
                }
            }
        }*/
        /*catch (Exception ex)
        {
            
        }*/

        await DisplayAlert("Error", $"An error occurred: MAKE ME FUNCTIONAL JACK", "OK");
    }
}