using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using COMP3404_Client.SaveLoadManagerScripts;
using Microsoft.Maui.Controls;

namespace COMP3404_Client.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    public ICommand SaveChatMessagesLocal {  get; private set; }
    //public ICommand SaveChatMessagesOnline { get; private set; }
    public ICommand SendChatMessage { get; private set; }

    SaveLoadManager saveLoadManager;

    public event PropertyChangedEventHandler PropertyChanged;

    private List<MessageViewModel> m_messages = [
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way", IsSender = true },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        ];
    public List<MessageViewModel> Messages
    {
        get => m_messages;
        set
        {
            m_messages = value;
            OnPropertyChanged();
        }
    }

    public MainPageViewModel() : base()
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
