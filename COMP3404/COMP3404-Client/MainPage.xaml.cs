namespace COMP3404_Client;

public partial class MainPage : ContentPage
{
    //TTS class;
    TTS tts;

    public MainPage()
    {
        InitializeComponent();

        tts = new TTS(new TTSSettings(true, 0, 100, 
            System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Adult));
    }

    private void SendButtonClicked(object sender, EventArgs e)
    {
        TTS.instance.Speak(chatInputFrame.Text);
        //TestThing.Message = chatInputFrame.Text;
    }
}
