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
            // shell nav to settings page
            await Shell.Current.GoToAsync("///"+nameof(SettingsPage));
        }

        private async void OnHistoryButtonClicked(object sender, EventArgs e)
        {
            // shell nav to history page
            await Shell.Current.GoToAsync("///" + nameof(HistoryPage));
        }

    }

}
