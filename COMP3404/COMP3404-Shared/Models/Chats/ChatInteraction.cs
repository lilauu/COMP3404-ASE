namespace COMP3404_Shared.Models.Chats;

public class ChatInteraction
{
    public string Message { get; set; }
    public string Response { get; set; }
    public DateTime Timestamp { get; set; }

    public ChatInteraction(string message, string response)
    {
        Message = message;
        Response = response;
        Timestamp = DateTime.Now;
    }
}
