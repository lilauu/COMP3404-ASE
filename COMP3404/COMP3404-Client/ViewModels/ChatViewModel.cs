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
/// A ViewModel representing a Chat
/// </summary>
public class ChatViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// See <seealso cref="INotifyPropertyChanged.PropertyChanged"/>
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// A command to save the current Chat. Takes an <see cref="IStorageService"/> parameter.
    /// </summary>
    public ICommand SaveChatMessages { get; private set; }
    /// <summary>
    /// A command to send a message to the current Chat. Takes a <see cref="string"/> parameter.
    /// </summary>
    public ICommand SendChatMessage { get; private set; }

    /// <summary>
    /// For View Bindings, exposes a storage service for storing online.
    /// </summary>
    public IStorageService ServerStorageService { get; private set; }
    /// <summary>
    /// For View Bindings, exposes a storage service for storing locally.
    /// </summary>
    public IStorageService DiskStorageService { get; private set; }

    /// <summary>
    /// The current name representing the Chat
    /// </summary>
    public string ChatName
    {
        get => m_chat.ChatName;
        set
        {
            m_chat.ChatName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Whether the current Chat is waiting for a response from the AI.
    /// </summary>
    public bool WaitingForResponse
    {
        get => m_waitingForResponse;
        private set
        {
            m_waitingForResponse = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Whether the current Chat is ready for user input.
    /// </summary>
    public bool ReadyForInput => WaitingForResponse;

    /// <summary>
    /// A collection of the current Chat's messages.
    /// </summary>
    public ObservableCollection<MessageViewModel> Messages { get; private set; } = [];

    private IAIModelService m_modelService;
    private TTSService m_ttsService;
    private bool m_waitingForResponse = false;
    private readonly Chat m_chat;

    /// <summary>
    /// Constructor for <see cref="ChatViewModel"/>. Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
    /// <param name="chat">The <see cref="Chat"/> that this ViewModel is representing</param>
    public ChatViewModel(Chat chat, IAIModelService modelService, DiskStorageService diskStorageService, ServerStorageService serverStorageService, TTSService ttsService)
    {
        m_ttsService = ttsService;
        m_modelService = modelService;
        DiskStorageService = diskStorageService;
        ServerStorageService = serverStorageService;

        m_chat = chat;
        SaveChatMessages = new Command<IStorageService>(SaveChat);
        SendChatMessage = new Command<string>(SendMessage);

        // populate messages 
        AddChatMessages(m_chat.Messages);

    }

    /// <summary>
    /// Attempt to translate a given message into the target language.
    /// </summary>
    /// <param name="message">The given message</param>
    /// <param name="language">The target language</param>
    public void TranslateMessage(string message, string language)
    {
        m_modelService.GetResponse($"Translate this statment ''{message}'' into {language}", m_chat, OnResponseReceived);
    }

    private void AddChatMessages(IEnumerable<ChatMessage> messages)
    {
        foreach (var message in messages)
            Messages.Add(new MessageViewModel(message.Message, message.IsHumanSender));
    }

    private void SaveChat(IStorageService manager)
    {
        manager.SaveChat(m_chat);
    }

    private void SendMessage(string message)
    {
        if (WaitingForResponse)
            return;

        if (string.IsNullOrWhiteSpace(message))
            return;

        var cm = new ChatMessage(message, true);
        m_chat.Messages.Add(cm);
        AddChatMessages([cm]);
        
        OnPropertyChanged(nameof(Messages));

        // lock button until a response is received
        WaitingForResponse = true;
        // todo: ask AI model for a response
        m_modelService.GetResponse(message, m_chat, OnResponseReceived);
    }

    private void OnResponseReceived(string response)
    {
        WaitingForResponse = false;
        var cm = new ChatMessage(response, false);
        m_chat.Messages.Add(cm);
        AddChatMessages([cm]);
        m_ttsService.Speak(response);
    }

    /// <summary>
    /// Helper function for invoking <see cref="PropertyChanged"/>
    /// </summary>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
