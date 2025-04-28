using COMP3404_Shared.Models.Chats;
using System.Net.Http.Json;
using System.Text.Json;

namespace COMP3404_Client.Services.AI;

/// <summary>
/// Service for communicating with the Gemini LLM.
/// </summary>
class GeminiAIModelService : IAIModelService
{
    private HttpClient m_httpClient;

    const string GeminiApiModelsUrl = "https://generativelanguage.googleapis.com/v1beta/models";
    // really bad to have this hardcoded, very insecure. This should really be got from the API server
    // where it is stored in the user secrets or something.
    const string GeminiApiKey = "AIzaSyD7LXfcRukECXJJAQ4mzGQXedZFh9kljc4";

    /// <summary>
    /// Constructor for <see cref="GeminiAIModelService"/> Typically uses Dependency Injection to resolve the required parameters.
    /// </summary>
    /// <param name="httpClient">An <see cref="HttpClient"/>, used for making API requests</param>
    /// <param name="serverService">Service for connecting to the COMP3404 server</param>
    public GeminiAIModelService(HttpClient httpClient, ServerService serverService)
    {
        m_httpClient = httpClient;
        m_serverService = serverService;
    }

    /// <summary>
    /// Get a response from the Gemini LLM. Calls <paramref name="onResponseReceived"/> when a response is returned.
    /// </summary>
    /// <param name="message">The prompt for the LLM</param>
    /// <param name="conversation">The ongoing conversation with the LLM</param>
    /// <param name="onResponseReceived">A callback, which is called with the LLM's response as a parameter when a response is returned.</param>
    public async void GetResponse(string message, Chat conversation, Action<string> onResponseReceived)
    {
        // formulate the body of the request, uses a lot of anonymous types
        var content = JsonContent.Create(new
        {
            contents = conversation.Messages
                .Append(new(message, true)) // add the new message to the end of the list
                .Select(m => new
                {
                    role = m.IsHumanSender ? "user" : "model",
                    parts = new[]
                    {
                        new
                        {
                            text = m.Message,
                        }
                    }
                }),
            generationConfig = new
            {
                maxOutputTokens = 100,
            },
            system_instruction = new
            {
                // system prompts, perhaps should be loaded from a config of some kind
                parts = new[]
                {
                    new
                    {
                        text = "Your name is NovaChat. You are a celestial body in the AI cosmos. You like to make astronomy-related puns and jokes."
                    },
                    new
                    {
                        // Our UI currently doesn't support things like markdown, so attempt to reduce that
                        text = "Respond in plain text only."
                    },
                    new
                    {
                        text = string.IsNullOrEmpty(m_serverService.GetFirstName()) ? "The user has not specified their name." : $"The user's name is {m_serverService.GetFirstName()}"
                    }
                }
            }
        });

        var response = await GetResponseFromApi("gemini-2.0-flash:generateContent", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseContent);
            var text = json.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
            onResponseReceived(text?.Trim() ?? "null");
        }
        else
        {
            // todo: add failure callback?
            onResponseReceived($"There was an error! {response.ReasonPhrase}");
        }
    }

    // helper function for forming http requests to the gemini api
    private async Task<HttpResponseMessage> GetResponseFromApi(string model, HttpContent content)
    {
        string targetUri = $"{GeminiApiModelsUrl}/{model}?key={GeminiApiKey}";
        var request = new HttpRequestMessage(HttpMethod.Post, targetUri);
        request.Content = content;
        return await m_httpClient.SendAsync(request);
    }

    public void TranslateMessage(string message, string language, Chat conversation, Action<string> onResponseReceived)
    {
        throw new NotImplementedException();
    }
}
