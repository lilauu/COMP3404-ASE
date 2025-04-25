using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using COMP3404_Client.SaveLoadManagerScripts;
using Microsoft.Maui.Controls;

namespace COMP3404_Client.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{


    public MainPageViewModel()
    {
        saveLoadManager = new();
        SaveChatMessagesLocal = new Command(SaveToFile);
        SendChatMessage = new Command<string>(SendMessage);
        //saveChatMessagesOnline = new Command<string>((key) => InputString += key);
    }

}
