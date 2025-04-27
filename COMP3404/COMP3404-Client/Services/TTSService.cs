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
    IPreferences m_preferences;
    #endregion

    public TTSService(IPreferences preferences)
    {
        m_preferences = preferences;
    }

    #region Methods
    //Speaks a string input

    public void Speak(string toSpeak)
    {
        if (!m_preferences.Get("Enabled", false))
            return;

        CancelSpeech();
        cts = new CancellationTokenSource();
        SpeechOptions opt = new()
        {
            Locale = TextToSpeech.Default.GetLocalesAsync().Result.FirstOrDefault(),
            Volume = m_preferences.Get("Volume", 1f),
            Pitch = m_preferences.Get("Pitch", 0f),
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
