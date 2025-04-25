using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using COMP3404_Client.SaveLoadManagerScripts;
using Microsoft.Maui.Controls;

namespace COMP3404_Client.ViewModels;

/// <summary>
/// The viewmodel for the main page of the app
/// </summary>
public class MainPageViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// The command that runs the save data to file method on button press from the page
    /// </summary>
    public ICommand SaveChatMessagesLocal {  get; private set; }
    //public ICommand SaveChatMessagesOnline { get; private set; }
    /// <summary>
    /// The command that runs the send message method on button press from the page
    /// </summary>
    public ICommand SendChatMessage { get; private set; }

    SaveLoadManager saveLoadManager;

    /// <summary>
    /// The event that fires when the setter changes a value for a message
    /// </summary>
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

    /// <summary>
    /// The list of messages displayed in the main page
    /// </summary>
    public List<MessageViewModel> Messages
    {
        get => m_messages;
        set
        {
            m_messages = value;
            OnPropertyChanged();
        }
    }
    /// <summary>
    /// The constructor for the view model that instantiates the save load manager and assigns the commands for saving and sending a chat message
    /// </summary>
    public MainPageViewModel()
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
    /// <summary>
    /// Invokes the PropertyChanged event
    /// </summary>
    /// <param name="name"></param>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
