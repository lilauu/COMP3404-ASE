using Microsoft.Maui.Storage;
using System.Text.Json;

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
    CancellationTokenSource cts;
    public SpeechOptions options;
    public bool enabled;
    #endregion

    #region Constructors
    //Blank constructor
    public TTS()
    {
        if (instance == null)
        {
            instance = this;
        }
        else return;

        options = new();
        LoadSettings();
        GetLocales();
    }
    #endregion

    #region Methods
    //Speaks a string input

    void LoadSettings()
    {
        options.Volume = Preferences.Get("Volume", 2f);
        options.Pitch = Preferences.Get("Pitch", 0f);
    }

    public async Task Speak(string toSpeak)
    {
        if (!enabled)
            return;

        CancelSpeech();
        cts = new CancellationTokenSource();
        await TextToSpeech.Default.SpeakAsync(toSpeak, options, cancelToken: cts.Token);

        // This method will block until utterance finishes.
    }

    // Cancel speech if a cancellation token exists & hasn't been already requested.
    public void CancelSpeech()
    {
        if (cts?.IsCancellationRequested ?? true)
            return;

        cts.Cancel();
    }

    async void GetLocales()
    {
        IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

        options.Locale = locales.FirstOrDefault();
    }
    #endregion
}
