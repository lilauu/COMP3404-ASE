namespace COMP3404_Server;

public static class Utils
{
    /// <summary>
    /// Gets the user's access token from the request header, placing it in <paramref name="token"/>
    /// </summary>
    /// <param name="headers"></param>
    /// <param name="token">The resulting token, or an empty string</param>
    /// <returns>Whether the token was successfully retrieved from the header</returns>
    public static bool GetUserTokenFromHeaders(IHeaderDictionary headers, out string token)
    {
        token = "";

        if (!headers.TryGetValue("Authorize", out var authoriseHeader))
            return false;

        if (authoriseHeader.Count != 1)
            return false;

        var splitted = authoriseHeader.ToString().Split(" ");
        if (splitted.Length != 2)
            return false;

        var accessToken = splitted[1];
        if (accessToken is null)
            return false;

        token = accessToken;
        return true;
    }
}
