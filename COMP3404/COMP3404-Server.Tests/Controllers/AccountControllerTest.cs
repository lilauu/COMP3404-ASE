using COMP3404_Server.Controllers;
using COMP3404_Server.Repositories;
using COMP3404_Shared.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System.Security;
using System.Text.Json;

namespace COMP3404_Server.Tests.Controllers;

public class AccountControllerTest
{
    static string s_sampleAuthResponse = """
        {
            "access_token":"gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "scope":"repo,gist",
            "token_type":"bearer"
        }
        """;
    static string s_sampleUserResponse = """
        {
            "login": "ASpoonPlaysGames",
            "id": 66967891,
            "node_id": "MDQ6VXNlcjY2OTY3ODkx",
            "avatar_url": "https://avatars.githubusercontent.com/u/66967891?v=4",
            "gravatar_id": "",
            "url": "https://api.github.com/users/ASpoonPlaysGames",
            "html_url": "https://github.com/ASpoonPlaysGames",
            "followers_url": "https://api.github.com/users/ASpoonPlaysGames/followers",
            "following_url": "https://api.github.com/users/ASpoonPlaysGames/following{/other_user}",
            "gists_url": "https://api.github.com/users/ASpoonPlaysGames/gists{/gist_id}",
            "starred_url": "https://api.github.com/users/ASpoonPlaysGames/starred{/owner}{/repo}",
            "subscriptions_url": "https://api.github.com/users/ASpoonPlaysGames/subscriptions",
            "organizations_url": "https://api.github.com/users/ASpoonPlaysGames/orgs",
            "repos_url": "https://api.github.com/users/ASpoonPlaysGames/repos",
            "events_url": "https://api.github.com/users/ASpoonPlaysGames/events{/privacy}",
            "received_events_url": "https://api.github.com/users/ASpoonPlaysGames/received_events",
            "type": "User",
            "user_view_type": "public",
            "site_admin": false,
            "name": "Jack",
            "company": null,
            "blog": "",
            "location": "Worcestershire, United Kingdom",
            "email": null,
            "hireable": null,
            "bio": "I mess around with code and also contribute to Northstar for Titanfall 2 :)\r\n\r\n\r\nCurrently a university student studying Computing.",
            "twitter_username": "SpoonPlays",
            "notification_email": null,
            "public_repos": 31,
            "public_gists": 0,
            "followers": 15,
            "following": 2,
            "created_at": "2020-06-15T17:55:11Z",
            "updated_at": "2025-04-07T12:26:44Z"
        }
        """;

    [Fact]
    public void GithubLogin_Success_AccountExists()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.Add(It.IsAny<UserAccount>()))
            .Returns<UserAccount>((ua) => ua); // just return whatever was passed in
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test_FirstName",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });

        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                var requestUri = request.RequestUri?.ToString();
                if (requestUri?.StartsWith("https://github.com/login/oauth/access_token") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(s_sampleAuthResponse) };
                }
                else if (requestUri?.StartsWith("https://api.github.com/user") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(s_sampleUserResponse) };
                }

                throw new Exception($"Unhandled request with URI {request.RequestUri}");
            });
        var client = new HttpClient(messageHandlerMock.Object);

        AccountController controller = new(accountRepoMock.Object, client);

        // Act
        ///////

        ActionResult<string> result = controller.GithubLogin("test");

        // Assert
        //////////
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("Jack", ((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public void GithubLogin_Success_NewAccount()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.Add(It.IsAny<UserAccount>()))
            .Returns<UserAccount>((ua) => ua); // just return whatever was passed in
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => null); // note that this will break if called after Add

        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                var requestUri = request.RequestUri?.ToString();
                if (requestUri?.StartsWith("https://github.com/login/oauth/access_token") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(s_sampleAuthResponse) };
                }
                else if (requestUri?.StartsWith("https://api.github.com/user") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(s_sampleUserResponse) };
                }

                throw new Exception($"Unhandled request with URI {request.RequestUri}");
            });
        var client = new HttpClient(messageHandlerMock.Object);

        AccountController controller = new(accountRepoMock.Object, client);

        // Act
        ///////

        ActionResult<string> result = controller.GithubLogin("test");

        // Assert
        //////////

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("Jack", ((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public void GithubLogin_Failure_BadAuthResponse()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.Add(It.IsAny<UserAccount>()))
            .Returns<UserAccount>((ua) => ua); // just return whatever was passed in
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });

        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                var requestUri = request.RequestUri?.ToString();
                if (requestUri?.StartsWith("https://github.com/login/oauth/access_token") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound) { Content = new StringContent("") };
                }
                else if (requestUri?.StartsWith("https://api.github.com/user") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(s_sampleUserResponse) };
                }

                throw new Exception($"Unhandled request with URI {request.RequestUri}");
            });
        var client = new HttpClient(messageHandlerMock.Object);

        AccountController controller = new(accountRepoMock.Object, client);

        // Act
        ///////

        ActionResult<string> act() => controller.GithubLogin("test");

        // Assert
        //////////

        Assert.Throws<SecurityException>(act);
    }

    [Fact]
    public void GithubLogin_Failure_MalformedAuthResponse()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.Add(It.IsAny<UserAccount>()))
            .Returns<UserAccount>((ua) => ua); // just return whatever was passed in
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });

        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                var requestUri = request.RequestUri?.ToString();
                if (requestUri?.StartsWith("https://github.com/login/oauth/access_token") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent("{malformedJson\":3}") };
                }
                else if (requestUri?.StartsWith("https://api.github.com/user") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(s_sampleUserResponse) };
                }

                throw new Exception($"Unhandled request with URI {request.RequestUri}");
            });
        var client = new HttpClient(messageHandlerMock.Object);

        AccountController controller = new(accountRepoMock.Object, client);

        // Act
        ///////

        ActionResult<string> act() => controller.GithubLogin("test");

        // Assert
        //////////

        Assert.Throws<JsonException>(act);
    }

    [Fact]
    public void GithubLogin_Failure_BadUserResponse()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.Add(It.IsAny<UserAccount>()))
            .Returns<UserAccount>((ua) => ua); // just return whatever was passed in
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });

        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                var requestUri = request.RequestUri?.ToString();
                if (requestUri?.StartsWith("https://github.com/login/oauth/access_token") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(s_sampleAuthResponse) };
                }
                else if (requestUri?.StartsWith("https://api.github.com/user") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden) { Content = new StringContent("") };
                }

                throw new Exception($"Unhandled request with URI {request.RequestUri}");
            });
        var client = new HttpClient(messageHandlerMock.Object);

        AccountController controller = new(accountRepoMock.Object, client);

        // Act
        ///////

        ActionResult<string> act() => controller.GithubLogin("test");

        // Assert
        //////////

        Assert.Throws<Exception>(act);
    }

    [Fact]
    public void GithubLogin_Failure_MalformedUserResponse()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.Add(It.IsAny<UserAccount>()))
            .Returns<UserAccount>((ua) => ua); // just return whatever was passed in
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });

        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                var requestUri = request.RequestUri?.ToString();
                if (requestUri?.StartsWith("https://github.com/login/oauth/access_token") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(s_sampleAuthResponse) };
                }
                else if (requestUri?.StartsWith("https://api.github.com/user") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent("{malformedJson\":3}") };
                }

                throw new Exception($"Unhandled request with URI {request.RequestUri}");
            });
        var client = new HttpClient(messageHandlerMock.Object);

        AccountController controller = new(accountRepoMock.Object, client);

        // Act
        ///////

        ActionResult<string> act() => controller.GithubLogin("test");

        // Assert
        //////////

        Assert.Throws<JsonException>(act);
    }
}
