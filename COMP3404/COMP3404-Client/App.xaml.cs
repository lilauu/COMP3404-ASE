using System.Security.Cryptography.X509Certificates;

namespace COMP3404_Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
#if (WINDOWS)
        //Create a new TTS class to assign the singleton referance
        TTS tts = new(new TTSSettings(true, 0, 100,
        System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Adult));

#endif

        MainPage = new AppShell();
    }
}
