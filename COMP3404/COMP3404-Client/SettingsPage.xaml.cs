namespace COMP3404_Client;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        AccountManager.instance.DoGitHubAuth();
    }
}