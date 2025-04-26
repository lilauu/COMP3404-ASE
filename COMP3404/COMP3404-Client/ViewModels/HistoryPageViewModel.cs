using COMP3404_Client.Services;
using COMP3404_Client.Services.AI;
using COMP3404_Client.Services.Storage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace COMP3404_Client.ViewModels;

public class HistoryPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand SwitchChatWindow { get; private set; }
    public ICommand Refresh { get; private set; }

    private ChatViewModel m_activeChat;
    public ChatViewModel ActiveChat
    {
        get => m_activeChat;
    }

    public IEnumerable<ChatViewModel> Chats => m_chats;

    private ObservableCollection<ChatViewModel> m_chats = new();

    private DiskStorageService m_diskStorageService;
    private ServerStorageService m_serverStorageService;

    public HistoryPageViewModel(DiskStorageService diskStorageService, ServerStorageService serverStorageService)
    {
        m_diskStorageService = diskStorageService;
        m_serverStorageService = serverStorageService;

        SwitchChatWindow = new Command<ChatViewModel>(SetActiveChat);
        Refresh = new Command(RefreshChats);

        RefreshChats();
    }

    private async void RefreshChats()
    {
        m_chats.Clear();
        var chatsFromDisk = await m_diskStorageService.LoadChatsAsync();
        var chatsFromServer = await m_serverStorageService.LoadChatsAsync();

        foreach (var chat in chatsFromDisk.Concat(chatsFromServer))
        {
            var viewModel = new ChatViewModel(chat, MauiProgram.GetService<IAIModelService>(), m_diskStorageService, m_serverStorageService, MauiProgram.GetService<TTSService>());
            m_chats.Add(viewModel);
        }
    }

    private void SetActiveChat(ChatViewModel chat)
    {
        m_activeChat = chat;
        OnPropertyChanged(nameof(ActiveChat));
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
