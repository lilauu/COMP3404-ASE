using System.Text.Json.Serialization;

namespace COMP3404_Shared.Models.Api;

/// <summary>
/// JSON structure representing data about a user.
/// </summary>
public class UserInfo
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = default!;

    // add new user info stuff here

}
