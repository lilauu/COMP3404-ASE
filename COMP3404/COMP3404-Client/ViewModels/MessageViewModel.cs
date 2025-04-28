using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COMP3404_Client.ViewModels;

/// <summary>
/// ViewModel representing a chat message.
/// </summary>
public class MessageViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// See <seealso cref="INotifyPropertyChanged.PropertyChanged"/>
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    private string m_displayMessage = "";
    private string m_fullMessage = "";
    private readonly Timer m_timer;
    private readonly Random m_rand;
    private bool m_isSender = false;

    /// <summary>
    /// Whether the current message was sent by the user.
    /// </summary>
    public bool IsSender
    {
        get => m_isSender;
        set
        {
            m_isSender = value;
            if (m_isSender)
                m_displayMessage = m_fullMessage;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// The current message's string message.
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
    /// Constructor for <see cref="MessageViewModel"/>
    /// </summary>
    /// <param name="message">The string content of the message</param>
    /// <param name="isSender">Whether the message was sent by the user</param>
    public MessageViewModel(string message, bool isSender)
    {
        m_isSender = isSender;
        m_fullMessage = message;

        if (isSender)
        {
            // set the display message directly to avoid having it be "typed" by the model
            m_displayMessage = message;
        }
        else
        {
            // simulate the AI "typing"
            m_timer = new Timer(new TimerCallback(OnTimerTick), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.2));
            m_rand = new Random();
        }
    }

    private void OnTimerTick(object? sender)
    {
        if (m_fullMessage == m_displayMessage)
            return;

        int startIndex = m_displayMessage.Length;
        int length = int.Min(m_rand.Next(8), m_fullMessage.Length - startIndex);
        // grab the next few characters from the full message
        string toAdd = m_fullMessage.Substring(startIndex, length);

        // add them to the display message
        m_displayMessage += toAdd;
        OnPropertyChanged(nameof(Message));
    }


    /// <summary>
    /// Helper function for invoking <see cref="PropertyChanged"/>
    /// </summary>
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
