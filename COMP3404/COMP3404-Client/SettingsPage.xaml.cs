using COMP3404_Client.Services;
using COMP3404_Client.ViewModels;
using COMP3404_Shared.Models.Api;

namespace COMP3404_Client;

public partial class SettingsPage : ContentPage
{
    private ServerService m_serverService;
    private SettingsPageViewModel m_viewModel;
    public SettingsPage(SettingsPageViewModel viewModel, ServerService serverService)
    {
        m_serverService = serverService;
        m_viewModel = viewModel;
        BindingContext = m_viewModel;
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (!await m_serverService.Authenticate())
        {
            await DisplayAlert("Login Failed!", "OH NO", "Ok.");
            return;
        }
        UserInfo? ui = await m_serverService.GetUserInfo();
        if (ui is null)
        {
            await DisplayAlert("Get user info failed!", "What? How?", "Weird.");
            return;
        }

        await DisplayAlert("Logged in!", $"Hello, {ui.FirstName}", "Continue.");
    }
}