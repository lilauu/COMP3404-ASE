namespace COMP3404_Client;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        // settings page nav with shell
        await Shell.Current.GoToAsync("///" + nameof(MainPage));
    }

    private async void OnHistoryButtonClicked(object sender, EventArgs e)
    {
        // settings page nav with shell
        await Shell.Current.GoToAsync("///" + nameof(HistoryPage));
    }
}