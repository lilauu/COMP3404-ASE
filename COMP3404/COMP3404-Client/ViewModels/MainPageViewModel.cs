using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using COMP3404_Client.SaveLoad;
using Microsoft.Maui.Controls;

namespace COMP3404_Client.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand SwitchChatWindow { get; private set; }

    private int m_activeChatIndex;

    public ChatViewModel ActiveChat
    {
        get => chatViewModelList[m_activeChatIndex];
    }

    private List<ChatViewModel> chatViewModelList = new();

    public MainPageViewModel()
    {
        SwitchChatWindow = new Command<string>(SetActiveChat);
        // todo: load from API as well
        
    }

    private void SetActiveChat(string chatId)
    {
        if (!int.TryParse(chatId, out var res))
            return;
        if (res >= chatViewModelList.Count)
            return;
        m_activeChatIndex = res;
        OnPropertyChanged(nameof(ActiveChat));
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
