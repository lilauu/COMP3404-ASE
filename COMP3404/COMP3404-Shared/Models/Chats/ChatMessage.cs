using System.Text.Json.Serialization;

namespace COMP3404_Shared.Models.Chats;

public class ChatMessage
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Message { get; set; }
    public bool IsHumanSender { get; set; }
    public DateTime Timestamp { get; set; }

    public ChatMessage(string message, bool isHumanSender)
    {
        Message = message;
        IsHumanSender = isHumanSender;
        Timestamp = DateTime.Now;
    }
}
