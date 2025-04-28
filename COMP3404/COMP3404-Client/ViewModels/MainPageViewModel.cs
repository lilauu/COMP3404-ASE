using COMP3404_Client.Services;
using COMP3404_Client.Services.AI;
using COMP3404_Client.Services.Storage;
using COMP3404_Shared.Models.Chats;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace COMP3404_Client.ViewModels;

/// <summary>
/// ViewModel representing the main page.
/// </summary>
public class MainPageViewModel : INotifyPropertyChanged
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
    /// Command that creates a new chat.
    /// </summary>
    public ICommand CreateNewChat { get; private set; }

    /// <summary>
    /// The currentlt visible chat.
    /// </summary>
    public ChatViewModel ActiveChat => m_activeChat;

    /// <summary>
    /// A collection of all chats.
    /// </summary>
    public IEnumerable<ChatViewModel> Chats => m_chats;

    private ObservableCollection<ChatViewModel> m_chats = new();
    private ChatViewModel m_activeChat;
    private DiskStorageService m_diskStorageService;
    private ServerStorageService m_serverStorageService;

    /// <summary>
    /// Constructor for <see cref="MainPageViewModel"/>. Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
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
        var newChat = new ChatViewModel(new Chat() { ChatName = "New Chat" }, MauiProgram.GetService<IAIModelService>(), m_diskStorageService, m_serverStorageService, MauiProgram.GetService<TTSService>());
        m_chats.Add(newChat);
        SetActiveChat(newChat);
    }


    /// <summary>
    /// Helper function for invoking <see cref="PropertyChanged"/>
    /// </summary>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
