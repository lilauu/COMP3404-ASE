using System.Text.Json.Serialization;

namespace COMP3404_Shared.Models.Chats;

public class ChatMessage
{
    // todo: partial class would be nice here so that client doesn't get the server-specific parts?
    [JsonIgnore]
    public int ChatId { get; set; }
    [JsonIgnore]
    public virtual Chat? ChatInfo { get; set; }
    [JsonIgnore]
    public int Id { get; set; }
    [JsonPropertyName("Message")]
    public string Message { get; set; }
    [JsonPropertyName("isHumanSender")]
    public bool IsHumanSender { get; set; }
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    public ChatMessage(string message, bool isHumanSender)
    {
        Message = message;
        IsHumanSender = isHumanSender;
        Timestamp = DateTime.Now;
    }
}
