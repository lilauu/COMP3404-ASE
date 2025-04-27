using COMP3404_Client.Services.AI;
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
using COMP3404_Client.Services.Storage;
using COMP3404_Client.Services;

namespace COMP3404_Client.ViewModels;

public class ChatViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand SaveChatMessages { get; private set; }
    public ICommand SendChatMessage { get; private set; }

    public IStorageService ServerStorageService { get; private set; }
    public IStorageService DiskStorageService { get; private set; }

    private IAIModelService m_modelService;
    private TTSService m_ttsService;

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

    private void AddChatMessages(IEnumerable<ChatMessage> messages)
    {
        foreach (var message in messages)
            Messages.Add(new MessageViewModel(message.Message, message.IsHumanSender));
    }

    void SaveChat(IStorageService manager)
    {
        manager.SaveChat(m_chat);
    }

    void SendMessage(string message)
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

    public void TranslateMessage(string message, string language)
    {
        m_modelService.GetResponse($"Translate this statment ''{message}'' into {language}", m_chat, OnResponseReceived);
    }

    void OnResponseReceived(string response)
    {
        WaitingForResponse = false;
        var cm = new ChatMessage(response, false);
        m_chat.Messages.Add(cm);
        AddChatMessages([cm]);
        m_ttsService.Speak(response);
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
