using Microsoft.Maui.Controls;

namespace COMP3404_Client
{
    public partial class MainPage : ContentPage
    {
        // Colours for light / dark mode
        Color lightModeColor = Color.FromRgb(240, 240,240);
        Color darkModeColor = Color.FromArgb("#333333");

        //Bool dor light / dark mode
        bool lightMode = true;

        //TTS class;
        TTS tts;

        public MainPage()
        {
            InitializeComponent();
            tts = new TTS(new TTSSettings(true, 0, 100, 
                System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Adult));

            LightDarkToggle(false);
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
            LightDarkToggle(true);
        }

        private void SendButtonClicked(object sender, EventArgs e)
        {
            TTS.instance.Speak(chatInputFrame.Text);
        }

        void LightDarkToggle(bool toggleMode)
        {
            if (lightMode)
            {
                //Is dark mode - change to light
                verticalStackLayout.BackgroundColor = lightModeColor;
                topGrid.BackgroundColor = lightModeColor;
                mainContentGrid.BackgroundColor = lightModeColor;
                chatWindow.BackgroundColor = lightModeColor;
                chatInput.BackgroundColor = lightModeColor;
                topBar.BackgroundColor = lightModeColor;

                topBarLabel.TextColor = darkModeColor;
            }
            else
            {
                //Is light mode - change to dark
                verticalStackLayout.BackgroundColor = darkModeColor;
                topGrid.BackgroundColor = darkModeColor;
                mainContentGrid.Background = darkModeColor;
                chatWindow.BackgroundColor = darkModeColor;
                chatInput.BackgroundColor = darkModeColor;
                topBar.BackgroundColor = darkModeColor;

                topBarLabel.TextColor = lightModeColor;
            }

            if(toggleMode) lightMode = !lightMode;
        }

    }

}
