using COMP3404_Client.Services;
using COMP3404_Shared.Models.Api;

namespace COMP3404_Client;

/// <summary>
/// View representing the settings page, also referred to as the profile page
/// </summary>
public partial class SettingsPage : ContentPage
{
    private ServerService m_serverService;
    private SettingsPageViewModel m_viewModel;

    /// <summary>
    /// Constructor for <see cref="SettingsPage"/>. Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
    public SettingsPage(SettingsPageViewModel viewModel, ServerService serverService)
    {
        m_serverService = serverService;
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