using COMP3404_Shared.Models.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace COMP3404_Client.Services.AI;

class GeminiAIModelService : IAIModelService
{
    private HttpClient m_httpClient;

    const string GeminiApiModelsUrl = "https://generativelanguage.googleapis.com/v1beta/models";
    // really bad to have this hardcoded, very insecure. This should really be got from the API server
    // where it is stored in the user secrets or something.
    const string GeminiApiKey = "AIzaSyD7LXfcRukECXJJAQ4mzGQXedZFh9kljc4";

    public GeminiAIModelService(HttpClient httpClient)
    {
        m_httpClient = httpClient;
    }

    public async void GetResponse(string message, Chat conversation, Action<string> onResponseReceived)
    {
        var content = JsonContent.Create(new
        {
            contents = conversation.Messages
                .Append(new(message, true)) // add the new message to the end
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
                parts = new[]
                {
                    new
                    {
                        text = "Your name is NovaChat. You are a celestial body in the AI cosmos. You like to make astronomy-related puns and jokes."
                    },
                    new
                    {
                        text = "Respond in plain text only."
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
            onResponseReceived($"There was an error! {response.ReasonPhrase}");
        }
    }

    private async Task<HttpResponseMessage> GetResponseFromApi(string model, HttpContent content)
    {
        string targetUri = $"{GeminiApiModelsUrl}/{model}?key={GeminiApiKey}";
        var request = new HttpRequestMessage(HttpMethod.Post, targetUri);
        request.Content = content;
        var result = await m_httpClient.SendAsync(request);
        return result;
    }
}
