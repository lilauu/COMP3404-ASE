using COMP3404_Shared.Models.Accounts;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace COMP3404_Shared.Models.Chats;

/// <summary>
/// A record of a chat with an AI model. 
/// </summary>
public class Chat
{
    [JsonIgnore]
    public int ChatId { get; set; }

    [JsonIgnore]
    public int OwnerId { get; set; }

    [JsonIgnore]
    public UserAccount OwnerInfo { get; set; }

    [JsonPropertyName("chatName")]
    public string ChatName { get; set; }
    public ObservableCollection<ChatMessage> Messages { get; set; } = new();
}
