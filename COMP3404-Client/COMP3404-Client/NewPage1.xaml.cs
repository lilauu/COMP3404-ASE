#if ANDROID
using Android.App;
using Android.Content.PM;
#endif

using System.Diagnostics;

namespace COMP3404_Client;

#if ANDROID
[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
              Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
              DataScheme = CALLBACK_SCHEME)]
public class WebAuthenticationCallbackActivity : Microsoft.Maui.Authentication.WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = "comp3404";
}
#endif

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}

    private async void Thing()
    {
        WebAuthenticatorResult authResult = await WebAuthenticator.AuthenticateAsync(
        new Uri("https://github.com/login/oauth/authorize?client_id=Ov23li6gKzCpMMxUThEE"),
        new Uri("comp3404://login/github"));
        string accessToken = authResult?.AccessToken;

        // Do something with the token
        Debug.WriteLine(accessToken);
    }

    // github registration flow: (OAuth)
    // https://docs.github.com/en/apps/oauth-apps/building-oauth-apps/authorizing-oauth-apps#web-application-flow
    // 1. request identity
    // 2. redirect back to app
    // 3. exchange code for access token
    // 4. use token to access API
    // notes:
    // WebAuthenticator seems great, but doesnt work on windows. will need windows-specific code i think
    private async void GithubButton_Clicked(object sender, EventArgs e)
    {
        await Task.Run(Thing);

        // await Browser.OpenAsync("https://github.com/login/oauth/authorize?client_id=Ov23li6gKzCpMMxUThEE", BrowserLaunchMode.SystemPreferred);

    }
}
