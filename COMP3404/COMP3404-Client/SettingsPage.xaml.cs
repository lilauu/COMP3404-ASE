namespace COMP3404_Client;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        // shell nav to main page
        await Shell.Current.GoToAsync("///" + nameof(MainPage));
        TTS.instance.Speak("Home");
    }

    private async void OnHistoryButtonClicked(object sender, EventArgs e)
    {
        // shell nav to history page
        await Shell.Current.GoToAsync("///" + nameof(HistoryPage));
        TTS.instance.Speak("History");
    }

    private async void OnLoginClicked(object sender, EventArgs e)
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
        // todo: store this somewhere for future API requests
        string token = result.AccessToken;

        await DisplayAlert("Wow you are authed", "we should do something in UI about this :)", "OK");
    }
}