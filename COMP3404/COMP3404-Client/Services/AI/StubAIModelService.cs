using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.Services.AI;

/// <summary>
/// A stubbed implementation of <see cref="IAIModelService"/>. Responds with predetermined responses, incorporating the user's input.
/// </summary>
public class StubAIModelService : IAIModelService
{
    private ServerService m_serverService;

    /// <summary>
    /// Constructor for <see cref="StubAIModelService"/> Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
    /// <param name="serverService">Service for connecting to the COMP3404 server</param>
    public StubAIModelService(ServerService serverService)
    {
        m_serverService = serverService;
    }

    /// <summary>
    /// Gets a response from the "LLM" (Stub). Calls <paramref name="onResponseReceived"/> when a response is received from the stub (immediately).
    /// </summary>
    /// <param name="message">The prompt for the LLM</param>
    /// <param name="conversation">The ongoing conversation with the LLM</param>
    /// <param name="onResponseReceived">A callback, which is called with the LLM's response as a parameter when a response is returned.</param>
    public void GetResponse(string message, Chat conversation, Action<string> onResponseReceived)
    {
        // doesnt use the conversation at all
        string nameStr = m_serverService.GetFirstName();
        if (string.IsNullOrEmpty(nameStr))
            nameStr = "You";
        onResponseReceived($"My name is \"{nameStr}\" Respond to this statement \"{message}\" in 100 or fewer words");
    }

    public void TranslateMessage(string message, string language, Chat conversation, Action<string> onResponseReceived)
    {
        string nameStr = m_serverService.FirstName;
        if (string.IsNullOrEmpty(nameStr))
            nameStr = "You";
        onResponseReceived($"Translating this message: {message} into {language}");
    }
}
