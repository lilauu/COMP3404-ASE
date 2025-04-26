using Microsoft.Maui.Storage;
using System.Text.Json;

namespace COMP3404_Client.Services;

/// <summary>
/// This class reads a string input out to the user
/// </summary>

public class TTSService
{
    // Composed of TTSSettings
    #region Fields
    CancellationTokenSource? cts;
    public bool enabled;
    #endregion

    #region Methods
    //Speaks a string input

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

    // Cancel speech if a cancellation token exists & hasn't been already requested.
    public void CancelSpeech()
    {
        if (cts?.IsCancellationRequested ?? true)
            return;

        cts.Cancel();
    }
    #endregion
}
