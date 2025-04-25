using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.AI.Stub;

internal class StubAIModel : IAIModel
{
    public void GetResponse(string message, Chat conversation, Action<string> onResponseReceived)
    {
        // doesnt use the conversation at all
        onResponseReceived($"This is a stub response! You said: {message}");
    }
}
