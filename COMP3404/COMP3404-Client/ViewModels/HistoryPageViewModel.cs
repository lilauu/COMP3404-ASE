using COMP3404_Client.Services;
using COMP3404_Client.Services.AI;
using COMP3404_Client.Services.Storage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace COMP3404_Client.ViewModels;

/// <summary>
/// ViewModel representing the history page.
/// </summary>
public class HistoryPageViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// See <seealso cref="INotifyPropertyChanged.PropertyChanged"/>
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Command that switches the current chat window Takes a <see cref="ChatViewModel"/> parameter.
    /// </summary>
    public ICommand SwitchChatWindow { get; private set; }

    /// <summary>
    /// Command that refreshes the loaded chats.
    /// </summary>
    public ICommand Refresh { get; private set; }

    /// <summary>
    /// The currently visible chat.
    /// </summary>
    public ChatViewModel ActiveChat => m_activeChat;

    /// <summary>
    /// All loaded chats.
    /// </summary>
    public IEnumerable<ChatViewModel> Chats => m_chats;

    private ObservableCollection<ChatViewModel> m_chats = new();
    private ChatViewModel m_activeChat;
    private DiskStorageService m_diskStorageService;
    private ServerStorageService m_serverStorageService;

    /// <summary>
    /// Constructor for <see cref="HistoryPageViewModel"/>. Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
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


    /// <summary>
    /// Helper function for invoking <see cref="PropertyChanged"/>
    /// </summary>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
