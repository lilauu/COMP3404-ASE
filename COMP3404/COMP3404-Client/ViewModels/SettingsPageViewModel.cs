using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client.ViewModels;

public class SettingsPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private IPreferences m_preferences;

    public bool Enabled
    {
        get => m_preferences.Get("Enabled", false);

        set 
        {
            if (value == Enabled)
                return;
            m_preferences.Set("Enabled", value);
            OnPropertyChanged();
        } 
    }

    public float Volume
    {
        get => m_preferences.Get("Volume", 1f);

        set
        {
            if (value == Volume)
                return;
            m_preferences.Set("Volume", value);
            OnPropertyChanged();
        }
    }

    public float Pitch
    {
        get => m_preferences.Get("Pitch", 0f);

        set
        {
            if (value == Pitch)
                return;
            m_preferences.Set("Pitch", value);
            OnPropertyChanged();
        }
    }

    public SettingsPageViewModel(IPreferences preferences)
    {
        m_preferences = preferences;
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
