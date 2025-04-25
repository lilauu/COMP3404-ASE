using COMP3404_Client.AI.Stub;
using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.AI;

/// <summary>
/// The interface that UI etc. should use when interacting with the AI model.
/// </summary>
public interface IAIModel
{
    // TODO: Replace with Gemini once stub is gone, and/or make service pattern for this?
    /// <summary>
    /// Gets the AI model instance currently used by the application.
    /// </summary>
    public static IAIModel Instance => StubAIModel.Instance;

    /// <summary>
    /// Gets a response from the AI model.
    /// </summary>
    /// <param name="message">The message that the user has sent</param>
    /// <param name="conversation">The ongoing conversation, containing message history</param>
    /// <param name="onResponseReceived">A callback that is called when a response is received from the model, with a string parameter of the model's response</param>
    public void GetResponse(string message, Chat conversation, Action<string> onResponseReceived);


}
