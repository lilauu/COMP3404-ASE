using COMP3404_Client.Themes;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace COMP3404_Client;

public partial class MainPage : ContentPage
{
    //Bool for light / dark mode
    bool lightMode = true;

    ICollection<ResourceDictionary> mergedDictionaries;

    public MainPage()
    {
        InitializeComponent();

        LightDarkToggle(false);
    }
    private async void OnProfileButtonClicked(object sender, EventArgs e)
    {
        // shell nav to settings page
        await Shell.Current.GoToAsync("///"+nameof(SettingsPage));
        await TTS.instance.Speak("Profile");
    }

    private async void OnHistoryButtonClicked(object sender, EventArgs e)
    {
        // shell nav to history page
        await Shell.Current.GoToAsync("///" + nameof(HistoryPage));
        await TTS.instance.Speak("History");
    }

    private void LightDarkModeButtonClicked(object sender, EventArgs e)
    {
        LightDarkToggle(true);
    }

    private void SendButtonClicked(object sender, EventArgs e)
    {
        TTS.instance.Speak(chatInputFrame.Text);
    }

    void LightDarkToggle(bool toggleMode)
    {
        if (lightMode)
        {
            mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new DarkTheme());
            }
        }

        else
        {
            mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new LightTheme());
            }
        }
        

        if (toggleMode) lightMode = !lightMode;
    }

    // github registration flow: (OAuth)
    // https://docs.github.com/en/apps/oauth-apps/building-oauth-apps/authorizing-oauth-apps#web-application-flow
    // 1. request identity
    // 2. redirect back to app
    // 3. exchange code for access token
    // 4. use token to access API
    private async void DoGithubAuth()
    {
        Uri authUri = new("https://github.com/login/oauth/authorize?client_id=Ov23li6gKzCpMMxUThEE");
        Uri redirectUri = new("comp3404://login/github");
#if WINDOWS
        var result = await WinUIEx.WebAuthenticator.AuthenticateAsync(authUri, redirectUri);
#else
        var result = await WebAuthenticator.AuthenticateAsync(authUri, redirectUri);
#endif
        // the above somewhat works, but for whatever reason I can't get it to work when doing github -> API server -> uri handler
        // so i will settle for github -> uri handler -> processing -> api server

        if (!result.Properties.TryGetValue("code", out var code))
            return;

        HttpClient client = new();
        var tokenResult = await client.SendAsync(new(HttpMethod.Get, $"http://127.0.0.1:5093/account/login/github?code={code}"));
        string token = await tokenResult.Content.ReadAsStringAsync();
        Console.WriteLine(token);
    }

    private void Slider_Volume_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        TTS.instance.options.Volume = (float)e.NewValue;
    }

    private void Slider_Pitch_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        TTS.instance.options.Pitch = (float)e.NewValue;
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        TTS.instance.enabled = e.Value;
    }
}
