using Microsoft.Maui.Storage;
using System.Text.Json;

namespace COMP3404_Client;

/// <summary>
/// This class reads a string input out to the user
/// </summary>

internal class TTS
{
    /// <summary>
    /// The instance variable of the TTS class.
    /// </summary>
    public static TTS instance;

    //Composed of TTSSettings
    #region Fields
    CancellationTokenSource cts;
    public bool enabled;
    #endregion

    #region Constructors

    /// <summary>
    /// Constructor of the TTS class - Creates the instance of the singleton
    /// </summary>
    //Blank constructor
    public TTS()
    {
        if (instance == null)
        {
            instance = this;
        }
        else return;

    }
    #endregion

    #region Methods
    //Speaks a string input

    /// <summary>
    /// Speaks the input out to the user
    /// </summary>
    /// <param name="toSpeak"></param>
    public void Speak(string toSpeak)
    {
        if (!Preferences.Get("Enabled", false))
            return;

        CancelSpeech();
        cts = new CancellationTokenSource();
        SpeechOptions opt = new()
        {
            Locale = TextToSpeech.Default.GetLocalesAsync().Result.FirstOrDefault(),
            Volume = Preferences.Get("Volume", 1f),
            Pitch = Preferences.Get("Pitch", 0f),
        };
        TextToSpeech.Default.SpeakAsync(toSpeak, opt, cancelToken: cts.Token);

        // This method will block until utterance finishes.
    }

    /// <summary>
    /// Cancels the current speaking token if it is still running
    /// </summary>
    // Cancel speech if a cancellation token exists & hasn't been already requested.
    public void CancelSpeech()
    {
        if (cts?.IsCancellationRequested ?? true)
            return;

        cts.Cancel();
    }
    #endregion
}
