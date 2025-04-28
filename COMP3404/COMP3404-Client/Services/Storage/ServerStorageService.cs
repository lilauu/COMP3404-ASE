using COMP3404_Shared.Models.Chats;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace COMP3404_Client.Services.Storage;

/// <summary>
/// Service for saving/loading chats to the COMP3404 API server
/// </summary>
public class ServerStorageService : IStorageService
{
    private ServerService m_serverService;

    /// <summary>
    /// Constructor for the <see cref="ServerStorageService"/>.
    /// </summary>
    /// <param name="serverService">Required service, used for communicating with the COMP3404 API server</param>
    public ServerStorageService(ServerService serverService)
    {
        m_serverService = serverService;
    }

    /// <summary>
    /// Loads all saved chats from the server. Requires that <see cref="ServerService.IsAuthenticated"/> returns true.
    /// </summary>
    /// <returns>A Task that returns an Enumerable of all loaded chats</returns>
    public async Task<IEnumerable<Chat>> LoadChatsAsync()
    {
        // todo: don't silently fail?
        if (!m_serverService.IsAuthenticated())
            return [];

        // ping API to get chats from database
        var response = await m_serverService.MakeAuthenticatedRequest(HttpMethod.Get, new HttpRequestOptions(), "chat/all");

        if (!response.IsSuccessStatusCode)
            return [];

        // parse response
        string responseContent = await response.Content.ReadAsStringAsync();
        var parseResult = JsonSerializer.Deserialize<List<Chat>>(responseContent);
        return parseResult ?? [];
    }

    /// <summary>
    /// Saves a <see cref="Chat"/> to the server. Requires that <see cref="ServerService.IsAuthenticated"/> returns true.
    /// </summary>
    /// <param name="chat">The chat to save</param>
    public void SaveChat(Chat chat)
    {
        // todo: don't silently fail?
        if (!m_serverService.IsAuthenticated())
            return;

        Task.Run(async () =>
        {
            // ping api to save the chat
            var content = JsonContent.Create(chat.Messages);
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
