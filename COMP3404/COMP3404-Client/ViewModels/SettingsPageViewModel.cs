using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COMP3404_Client.ViewModels;

/// <summary>
/// ViewModel representing the settings page
/// </summary>
public class SettingsPageViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// See <seealso cref="INotifyPropertyChanged.PropertyChanged"/>
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    private IPreferences m_preferences;

    /// <summary>
    /// Whether TTS is enabled
    /// </summary>
    public bool TTSEnabled
    {
        get => m_preferences.Get("TTSEnabled", false);

        set 
        {
            if (value == TTSEnabled)
                return;
            m_preferences.Set("TTSEnabled", value);
            OnPropertyChanged();
        } 
    }

    /// <summary>
    /// The volume of the TTS
    /// </summary>
    public float TTSVolume
    {
        get => m_preferences.Get("TTSVolume", 1f);

        set
        {
            if (value == TTSVolume)
                return;
            m_preferences.Set("TTSVolume", value);
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// The pitch of the TTS
    /// </summary>
    public float TTSPitch
    {
        get => m_preferences.Get("TTSPitch", 0f);

        set
        {
            if (value == TTSPitch)
                return;
            m_preferences.Set("TTSPitch", value);
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Constructor for <see cref="SettingsPage"/>. Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
    public SettingsPageViewModel(IPreferences preferences)
    {
        m_preferences = preferences;
    }


    /// <summary>
    /// Helper function for invoking <see cref="PropertyChanged"/>
    /// </summary>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
