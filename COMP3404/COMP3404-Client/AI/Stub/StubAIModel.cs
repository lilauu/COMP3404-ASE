using COMP3404_Shared.Models.Chats;
using Windows.Globalization;

namespace COMP3404_Client.AI.Stub;

internal class StubAIModel : IAIModel
{
    public void GetResponse(string message, Conversation conversation, Action<string> onResponseReceived)
    {
        // doesnt use the conversation at all
        onResponseReceived($"This is a stub response! You said: {message}");
    }

    public void LimitNumberOfWordsInResponce(int numberOfWords, Conversation conversation, Action<string> onResponseReceived)
    {
        onResponseReceived($"This is a stub response! I will now limit the number of words I respond with to {numberOfWords}");
    }

    public void SetLanguage(string language, Conversation conversation, Action<string> onResponseReceived)
    {
        onResponseReceived($"This is a stub response! I will now display in {language}");
    }
}
