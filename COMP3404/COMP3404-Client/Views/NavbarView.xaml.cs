namespace COMP3404_Client.Views;

public partial class NavbarView : ContentView
{
	public NavbarView()
	{
		InitializeComponent();
	}

	private async void OnHomeButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///"+nameof(MainPage));
		TTS.instance.Speak("Home");
	}

	private async void OnProfileButtonClicked(object sender, EventArgs e)
	{
		// shell nav to settings page
		await Shell.Current.GoToAsync("///"+nameof(SettingsPage));
		TTS.instance.Speak("Profile");
	}

	private async void OnHistoryButtonClicked(object sender, EventArgs e)
	{
		// shell nav to history page
		await Shell.Current.GoToAsync("///" + nameof(HistoryPage));
		TTS.instance.Speak("History");
	}

	private void ThemeToggleButtonClicked(object sender, EventArgs e)
	{
		// get current theme from user settings
		var currentTheme = Application.Current.UserAppTheme;
		// if unspecified, get the current device theme
		if (currentTheme == AppTheme.Unspecified)
			currentTheme = Application.Current.RequestedTheme;
		// assume light mode is the current theme if none is specified
		if (currentTheme == AppTheme.Unspecified)
			currentTheme = AppTheme.Light;

		Application.Current.UserAppTheme = currentTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
	}
}