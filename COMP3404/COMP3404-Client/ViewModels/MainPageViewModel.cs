using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using COMP3404_Client.Services.AI;
using COMP3404_Shared.Models.Chats;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using COMP3404_Client.Services.Storage;

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

    private DiskStorageService m_diskStorageService;
    private ServerStorageService m_serverStorageService;

    public MainPageViewModel(DiskStorageService diskStorageService, ServerStorageService serverStorageService)
    {
        m_diskStorageService = diskStorageService;
        m_serverStorageService = serverStorageService;

        SwitchChatWindow = new Command<ChatViewModel>(SetActiveChat);
        CreateNewChat = new Command(CreateChat);

        CreateChat();
    }

    private void SetActiveChat(ChatViewModel chat)
    {
        m_activeChat = chat;
        OnPropertyChanged(nameof(ActiveChat));
    }

    private void CreateChat()
    {
        var newChat = new ChatViewModel(new Chat() { ChatName = "New Chat" }, MauiProgram.GetService<IAIModelService>(), m_diskStorageService, m_serverStorageService);
        chatViewModelList.Add(newChat);
        SetActiveChat(newChat);
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
