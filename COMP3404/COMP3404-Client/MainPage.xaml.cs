namespace COMP3404_Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }
        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            // settings page nav with shell
            await Shell.Current.GoToAsync("///"+nameof(SettingsPage));
        }

    }

}
