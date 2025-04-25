using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COMP3404_Client.ViewModels;

public class MessageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string m_displayMessage = "";
    private string m_fullMessage = "";
    private Timer m_timer;
    private Random m_rand;
    private bool m_isSender = false;

    public bool IsSender
    {
        get => m_isSender;
        set
        {
            m_isSender = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => m_displayMessage;
        set
        {
            m_fullMessage = value;
            m_displayMessage = "";
            OnPropertyChanged();
        }
    }

    public string FullMessage => m_fullMessage;

    public MessageViewModel()
    {
        m_timer = new Timer(new TimerCallback(OnTimerTick), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.2));
        m_rand = new Random();
    }

    private void OnTimerTick(object? sender)
    {
        if (m_fullMessage == m_displayMessage)
            return;

        int startIndex = m_displayMessage.Length;
        int length = int.Min(m_rand.Next(4), m_fullMessage.Length - startIndex);
        // grab the next few characters from the full message
        string toAdd = m_fullMessage.Substring(startIndex, length);

        // add them to the display message
        m_displayMessage += toAdd;
        OnPropertyChanged(nameof(Message));
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
