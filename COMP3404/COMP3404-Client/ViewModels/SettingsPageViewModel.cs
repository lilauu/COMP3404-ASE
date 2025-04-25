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

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
