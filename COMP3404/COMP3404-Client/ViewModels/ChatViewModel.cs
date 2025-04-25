using COMP3404_Client.SaveLoad;
using COMP3404_Shared.Models.Chats;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace COMP3404_Client.ViewModels;

public class ChatViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand SaveChatMessages { get; private set; }
    public ICommand SendChatMessage { get; private set; }

    public ISaveLoadManager DiskSaveLoadManager => SaveLoad.DiskSaveLoadManager.Instance;
    public ISaveLoadManager ServerSaveLoadManager => SaveLoad.ServerSaveLoadManager.Instance;

    public string ChatName
    {
        get => m_chat.ChatName;
        set
        {
            m_chat.ChatName = value;
            OnPropertyChanged();
        }
    }

    private bool m_waitingForResponse = false;
    public bool WaitingForResponse
    {
        get => m_waitingForResponse;
        private set
        {
            m_waitingForResponse = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<MessageViewModel> Messages { get; private set; } = [];


    private readonly Chat m_chat;

    public ChatViewModel(Chat chat)
    {
        m_chat = chat;
        SaveChatMessages = new Command<ISaveLoadManager>(SaveChat);
        SendChatMessage = new Command<string>(SendMessage);
        //saveChatMessagesOnline = new Command<string>((key) => InputString += key);

        // populate messages 
        AddChatMessages(m_chat.Messages);
        // todo: implement a way to tear down this class, we might be leaking by not deregistering this
        m_chat.Messages.CollectionChanged += ChatMessages_CollectionChanged;
    }

    private void ChatMessages_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null)
        {
            throw new NotImplementedException("Removing messages is unsupported");
        }
        if (e.NewItems != null)
        {
            AddChatMessages(e.NewItems.Cast<ChatMessage>());
        }
    }

    private void AddChatMessages(IEnumerable<ChatMessage> messages)
    {
        foreach (var message in messages)
            Messages.Add(new MessageViewModel(message.Message, message.IsHumanSender));
    }

    void SaveChat(ISaveLoadManager manager)
    {
        manager.SaveChat(m_chat);
    }

    void SendMessage(string message)
    {
        if (WaitingForResponse)
            return;

        if (string.IsNullOrWhiteSpace(message))
            return;

        m_chat.Messages.Add(new(message, true));
        OnPropertyChanged(nameof(Messages));

        // lock button until a response is received
        //WaitingForResponse = true;
        // todo: ask AI model for a response
    }

    void OnResponseReceived(string response)
    {
        WaitingForResponse = false;
        m_chat.Messages.Add(new(response, false));
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
