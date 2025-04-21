using System.Text.Json.Serialization;

namespace COMP3404_Shared.Models.Api.Github;

public class GetUserResponse
{
    // there's more to this but we don't need all of it

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }
}
