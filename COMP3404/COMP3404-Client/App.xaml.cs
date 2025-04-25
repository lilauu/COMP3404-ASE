using System.Security.Cryptography.X509Certificates;

namespace COMP3404_Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        TTS tts = new();
        TTS tts = new TTS(new TTSSettings(true, 0, 100,
           System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Adult));

#endif
        TTS tts = new TTS(new TTSSettings(true, 0, 100,
           System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Adult));

#endif
        TTS tts = new TTS(new TTSSettings(true, 0, 100,
           System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Adult));

#endif

        MainPage = new AppShell();
    }
}
