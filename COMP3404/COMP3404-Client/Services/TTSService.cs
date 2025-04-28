namespace COMP3404_Client.Services;

/// <summary>
/// Service for reading text aloud to the user.
/// </summary>
public class TTSService
{
    CancellationTokenSource? m_cancellationTokenSource;
    IPreferences m_preferences;

    /// <summary>
    /// Constructor for the <see cref="TTSService"/>. Typically uses Dependency Injection to resolve the required services.
    /// </summary>
    /// <param name="preferences">Required <see cref="IPreferences"/> service</param>
    public TTSService(IPreferences preferences)
    {
        m_preferences = preferences;
    }


    /// <summary>
    /// Reads the given text aloud as Text to Speech.
    /// </summary>
    /// <param name="toSpeak">The message to speak aloud.</param>
    public void Speak(string toSpeak)
    {
        if (!m_preferences.Get("TTSEnabled", false))
            return;

        CancelSpeech();
        m_cancellationTokenSource = new CancellationTokenSource();
        SpeechOptions opt = new()
        {
            Locale = TextToSpeech.Default.GetLocalesAsync().Result.FirstOrDefault(),
            Volume = m_preferences.Get("TTSVolume", 1f),
            Pitch = m_preferences.Get("TTSPitch", 0f),
        };
        TextToSpeech.Default.SpeakAsync(toSpeak, opt, cancelToken: m_cancellationTokenSource.Token);

        // This method will block until utterance finishes.
    }

    /// <summary>
    /// Cancels the current speech.
    /// </summary>
    public void CancelSpeech()
    {
        // Cancel speech if a cancellation token exists & hasn't been already requested.
        if (m_cancellationTokenSource?.IsCancellationRequested ?? true)
            return;

        m_cancellationTokenSource.Cancel();
    }
}
