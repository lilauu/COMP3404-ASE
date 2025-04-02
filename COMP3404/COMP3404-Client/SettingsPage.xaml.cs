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
    }

    private async void OnHistoryButtonClicked(object sender, EventArgs e)
    {
        // shell nav to history page
        await Shell.Current.GoToAsync("///" + nameof(HistoryPage));
    }
}