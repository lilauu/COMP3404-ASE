using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client.ViewModels;

/// <summary>
/// The viewmodel for the settings page in the app
/// </summary>
public class SettingsPageViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// A boolean that determines if the message was sent from the user or the AI
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// A boolean that controls whether the text to speech speaks or not
    /// </summary>
    public bool Enabled
    {
        get => Preferences.Get("Enabled", false);

        set 
        {
            if (value == Enabled)
                return;
            Preferences.Set("Enabled", value);
            OnPropertyChanged();
        } 
    }

    /// <summary>
    /// A float that controls how loud the text to speech is
    /// </summary>
    public float Volume
    {
        get => Preferences.Get("Volume", 1f);

        set
        {
            if (value == Volume)
                return;
            Preferences.Set("Volume", value);
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// A float that controls the pitch of the text to speech
    /// </summary>
    public float Pitch
    {
        get => Preferences.Get("Pitch", 0f);

        set
        {
            if (value == Pitch)
                return;
            Preferences.Set("Pitch", value);
            OnPropertyChanged();
        }
    }

    public string Language
    {
        get => Preferences.Get("Language", "English");

        set
        {
            if (value == Language)
                return;
            Preferences.Set("Language", value);
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Invokes the PropertyChanged event
    /// </summary>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
