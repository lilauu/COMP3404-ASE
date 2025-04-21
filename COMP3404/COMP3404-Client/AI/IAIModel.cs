using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.AI;

/// <summary>
/// The interface that UI etc. should use when interacting with the AI model.
/// </summary>
public interface IAIModel
{
    public void GetResponse(string message, Conversation conversation, Action<string> onResponseReceived);


}
