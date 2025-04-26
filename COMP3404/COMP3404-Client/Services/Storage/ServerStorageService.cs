using COMP3404_Shared.Models.Chats;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace COMP3404_Client.Services.Storage;

public class ServerStorageService : IStorageService
{
    private ServerService m_serverService;

    public ServerStorageService(ServerService serverService)
    {
        m_serverService = serverService;
    }

    public async Task<IEnumerable<Chat>> LoadChatsAsync()
    {
        // todo: don't silently fail?
        if (!m_serverService.IsAuthenticated())
            return new List<Chat>();

        // ping API to get chats from database
        var response = await m_serverService.MakeAuthenticatedRequest(HttpMethod.Get, new HttpRequestOptions(), "chat/all");

        if (!response.IsSuccessStatusCode)
            return new List<Chat>();

        // parse response
        string responseContent = await response.Content.ReadAsStringAsync();
        var parseResult = JsonSerializer.Deserialize<IEnumerable<Chat>>(responseContent);
        return parseResult ?? new List<Chat>();
    }

    public void SaveChat(Chat chat)
    {
        // todo: don't silently fail?
        if (!m_serverService.IsAuthenticated())
            return;

        Task.Run(async () =>
        {
            // ping api to save the chat
            var content = JsonContent.Create(chat.Messages.ToList());
            var query = HttpUtility.ParseQueryString(string.Empty);
            query.Add("chatName", chat.ChatName);

            var response = await m_serverService.MakeAuthenticatedRequest(HttpMethod.Post, new HttpRequestOptions(), $"chat?{query.ToString()}", content);

            response.EnsureSuccessStatusCode();
            // todo: logging of some kind?
            /*        if (!response.IsSuccessStatusCode)
                        return;*/
        });
    }
}
