using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using COMP3404_Client.SaveLoad;
using COMP3404_Shared.Models.Chats;
using Microsoft.Maui.Controls;

namespace COMP3404_Client.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand SwitchChatWindow { get; private set; }
    public ICommand CreateNewChat { get; private set; }

    private ChatViewModel m_activeChat;

    public ChatViewModel ActiveChat
    {
        get => m_activeChat;
    }

    public IEnumerable<ChatViewModel> Chats => chatViewModelList;

    private ObservableCollection<ChatViewModel> chatViewModelList = new();

    public MainPageViewModel()
    {
        SwitchChatWindow = new Command<ChatViewModel>(SetActiveChat);
        CreateNewChat = new Command(CreateChat);
        // todo: load from API as well

        // default new chat
        if (chatViewModelList.Count == 0)
            CreateChat();
    }

    private void SetActiveChat(ChatViewModel chat)
    {
        m_activeChat = chat;
        OnPropertyChanged(nameof(ActiveChat));
    }

    private void CreateChat()
    {
        var newChat = new ChatViewModel(new Chat() { ChatName = "New Chat" });
        chatViewModelList.Add(newChat);
        SetActiveChat(newChat);
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
