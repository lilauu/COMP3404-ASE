using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.AI;

/// <summary>
/// The interface that UI etc. should use when interacting with the AI model.
/// </summary>
public interface IAIModel
{
    /// <summary>
    /// Gets a response from the AI model.
    /// </summary>
    /// <param name="message">The message that the user has sent</param>
    /// <param name="chat">The ongoing conversation, containing message history</param>
    /// <param name="onResponseReceived">A callback that is called when a response is received from the model, with a string parameter of the model's response</param>
    public void GetResponse(string message, Chat chat, Action<string> onResponseReceived);

    public void TranslateLanguage(string message, string language, Chat chat, Action<string> onResponseReceived);
}

