using System.Speech.Synthesis;

namespace COMP3404_Client;

/// <summary>
/// This class reads a string input out to the user
/// </summary>

internal class TTS
{
    //Should not be using the singleton pattern probably
    public static TTS instance;

    //Composed of TTSSettings
    #region Fields
    TTSSettings settings;
    SpeechSynthesizer synth;
    #endregion

    #region Constructors
    //Blank constructor
    public TTS(TTSSettings pSettings)
    {
        if (instance == null)
        {
            instance = this;
        }
        else return;

        //Create a new SpeechSynthesizer
        synth = new SpeechSynthesizer();
        synth.SetOutputToDefaultAudioDevice();
        settings = pSettings;
    }
    #endregion

    #region Methods
    //Speaks a string input
    public void Speak(string toSpeak)
    {
        if (settings.Enabled && toSpeak != string.Empty)
        {
            synth.Rate = settings.Rate;
            synth.Volume = settings.Volume;
            synth.SelectVoiceByHints(settings.Gender, settings.Age);

            synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(toSpeak);
        }
    }
    //Pauses the SpeechSynthesizer
    public void Pause()
    {
        synth.Pause();
    }

    //Resumes the SpeechSynthesizer
    public void Resume()
    {
        synth.Resume();
    }

    #endregion
}