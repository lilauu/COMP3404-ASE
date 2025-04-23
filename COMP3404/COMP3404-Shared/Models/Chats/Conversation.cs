using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace COMP3404_Shared.Models.Chats;

/// <summary>
/// A record of a chat with an AI model. 
/// </summary>
public class Conversation
{
    [JsonIgnore]
    private readonly List<ChatInteraction> m_interactions = new();
    public IEnumerable<ChatInteraction> Interactions => m_interactions.AsReadOnly();

    public void AddInteraction(string message, string response)
    {
        m_interactions.Add(new ChatInteraction(message, response));
    }
}
