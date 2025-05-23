﻿using System.Text.Json.Serialization;

namespace COMP3404_Shared.Models.Api.Github;

public class GithubOAuthResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}
