using Microsoft.AspNetCore.Mvc;

namespace COMP3404_Server.Controllers;

/// <summary>
/// Stub controller for the index of the server, taking a page from Stryder's book :)
/// </summary>
[ApiController]
[Route("/")]
public class IndexController : ControllerBase
{
    /// <summary>
    /// Tells the user to go away, useful for testing since it's still an actual response
    /// </summary>
    [HttpGet]
    public string Get() => "Go away.";
}
