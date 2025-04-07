namespace COMP3404_Client
{
    public partial class MainPage : ContentPage
    {
        TTS tts;
        public MainPage()
        {
            InitializeComponent();
            tts = new TTS();
        }
        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            // shell nav to settings page
            await Shell.Current.GoToAsync("///"+nameof(SettingsPage));
            TTS.instance.Speak("Profile", 0);
        }

        private async void OnHistoryButtonClicked(object sender, EventArgs e)
        {
            // shell nav to history page
            await Shell.Current.GoToAsync("///" + nameof(HistoryPage));
            TTS.instance.Speak("History", 0);
        }

    }

}
