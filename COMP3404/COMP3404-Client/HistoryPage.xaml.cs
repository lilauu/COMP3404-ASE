
namespace COMP3404_Client;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
	}

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        // shell nav to main page
        await Shell.Current.GoToAsync("///" + nameof(MainPage));
        TTS.instance.Speak("Home", 0);
    }

    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        // shell nav to settings page
        await Shell.Current.GoToAsync("///" + nameof(SettingsPage));
        TTS.instance.Speak("Settings", 0);
    }
}