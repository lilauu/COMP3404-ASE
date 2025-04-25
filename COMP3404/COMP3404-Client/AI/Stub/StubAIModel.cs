using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.AI.Stub;

public class StubAIModel : IAIModel
{
    public static readonly StubAIModel Instance = new();

    public void GetResponse(string message, Chat conversation, Action<string> onResponseReceived)
    {
        // doesnt use the conversation at all
        onResponseReceived($"This is a stub response! You said: {message}");
    }
}
