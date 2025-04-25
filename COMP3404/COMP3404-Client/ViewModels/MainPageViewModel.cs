using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using COMP3404_Client.SaveLoadManagerScripts;

namespace COMP3404_Client.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    public ICommand SaveChatMessagesLocal {  get; private set; }
    //public ICommand saveChatMessagesOnline { get; private set; }

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
        //saveChatMessagesOnline = new Command<string>((key) => InputString += key);
    }

    void SaveToFile()
    {
        var messages = m_messages.Select(m => m.FullMessage);

        saveLoadManager.SaveDataToFile(messages, "test.txt");
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
