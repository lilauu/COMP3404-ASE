using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.Services.AI;

public class StubAIModelService : IAIModelService
{
    private ServerService m_serverService;

    public StubAIModelService(ServerService serverService)
    {
        m_serverService = serverService;
    }

    public void GetResponse(string message, Chat conversation, Action<string> onResponseReceived)
    {
        // doesnt use the conversation at all
        string nameStr = m_serverService.FirstName;
        if (string.IsNullOrEmpty(nameStr))
            nameStr = "You";
        onResponseReceived($"Respond to this statement ''{message}'' in 250 or fewer words");
    }
}
