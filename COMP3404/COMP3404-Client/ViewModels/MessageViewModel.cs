using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COMP3404_Client.ViewModels;

public class MessageViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// The viewmodel for a message sent in the app
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    private string m_displayMessage = "";
    private string m_fullMessage = "";
    private Timer m_timer;
    private Random m_rand;
    private bool m_isSender = false;

    /// <summary>
    /// A boolean that determines if the message was sent from the user or the AI
    /// </summary>
    public bool IsSender
    {
        get => m_isSender;
        set
        {
            m_isSender = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// A string that holds the state of the message as it is being typed back to the user (effect)
    /// </summary>
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

    /// <summary>
    /// The message sent to or from the user
    /// </summary>
    public string FullMessage => m_fullMessage;

    /// <summary>
    /// The constructor of the view model that  creates a new timer and random instance
    /// </summary>
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

    /// <summary>
    /// Invokes the PropertyChanged event
    /// </summary>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
