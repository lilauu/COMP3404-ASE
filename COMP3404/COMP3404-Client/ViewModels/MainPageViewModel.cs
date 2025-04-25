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

        RefreshChats();
    }

    // todo: call this somehow when login is complete
    public void RefreshChats()
    {
        Task.Run(async () =>
        {
            // load chats from file
            var chatsFromDisk = await DiskSaveLoadManager.Instance.LoadChatsAsync();
            foreach (var chat in chatsFromDisk)
            {
                var viewModel = new ChatViewModel(chat);
                chatViewModelList.Add(viewModel);
            }

            // default new chat
            if (chatViewModelList.Count == 0)
                CreateChat();
            else
                SetActiveChat(chatViewModelList[0]);
        });
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
