namespace COMP3404_Client;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
	}

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        // settings page nav with shell
        await Shell.Current.GoToAsync("///" + nameof(MainPage));
    }

    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///" + nameof(SettingsPage));
    }
}