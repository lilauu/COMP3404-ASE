using COMP3404_Client.SaveLoadManagerScripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace COMP3404_Client.ViewModels;

public class ChatViewModel : INotifyPropertyChanged
{
    public ICommand SaveChatMessagesLocal { get; private set; }
    //public ICommand SaveChatMessagesOnline { get; private set; }
    public ICommand SendChatMessage { get; private set; }

    SaveLoadManager saveLoadManager;

    public event PropertyChangedEventHandler PropertyChanged;

    private List<MessageViewModel> m_messages = [];

    public List<MessageViewModel> Messages
    {
        get => m_messages;
        set
        {
            m_messages = value;
            OnPropertyChanged();
        }
    }

    public ChatViewModel()
    {
        saveLoadManager = new();
        SaveChatMessagesLocal = new Command(SaveToFile);
        SendChatMessage = new Command<string>(SendMessage);
        //saveChatMessagesOnline = new Command<string>((key) => InputString += key);
    }

    void SaveToFile()
    {
        var messages = m_messages.Select(m => m.FullMessage);

        saveLoadManager.SaveDataToFile(messages, "test.txt");
    }

    void SendMessage(string message)
    {
        if (message?.Length > 0)
        {
            Messages.Add(new MessageViewModel() { Message = message, IsSender = true });
        }
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
