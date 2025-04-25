using COMP3404_Client.API;
using COMP3404_Shared.Models.Api;

namespace COMP3404_Client;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (!await DataManager.Instance.Authenticate())
        {
            await DisplayAlert("Login Failed!", "OH NO", "Ok.");
            return;
        }
        UserInfo? ui = await DataManager.Instance.GetUserInfo();
        if (ui is null)
        {
            await DisplayAlert("Get user info failed!", "What? How?", "Weird.");
            return;
        }

        await DisplayAlert("Logged in!", $"Hello, {ui.FirstName}", "Continue.");
    }
}