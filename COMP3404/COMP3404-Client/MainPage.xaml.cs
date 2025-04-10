namespace COMP3404_Client
{
    public partial class MainPage : ContentPage
    {
        bool lightMode = false;

        TTS tts;
        TTSSettings ttsSettings;

        public MainPage()
        {
            InitializeComponent();
            tts = new TTS(new TTSSettings(true, 0));
            
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

        private void LightDarkModeButtonClicked(object sender, EventArgs e)
        {
            // Does not work - Change background colour here
            if (lightMode)
            {
                //Is dark mode - change to light
                Shell.Current.BackgroundColor = Color.FromRgb(0, 0, 0);
            }
            else
            {
                //is light - change to dark
                Shell.Current.BackgroundColor = Color.FromRgb(255, 255, 255);
            }
            lightMode = !lightMode;
        }

        private void SendButtonClicked(object sender, EventArgs e)
        {

        }

    }

}
