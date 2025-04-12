using COMP3404_Server.Controllers;
using COMP3404_Server.Repositories;
using COMP3404_Shared.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Moq.Protected;

namespace COMP3404_Server.Tests.Controllers;

public class AccountControllerTest
{
    [Fact]
    public void GetGithubToken_Success()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
/*        accountRepoMock.Setup(r => r.GetById(-1)).Returns(new UserAccount()
        {
            AccountId = -1,
            FirstName = "TestName",
            GithubToken = "gho_16C7e42F292c6912E7710c830000Ae178B4a"
        });*/

        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                // initial auth request
                if (request.RequestUri?.ToString().StartsWith("https://github.com/login/oauth/access_token") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent("""
                        {
                          "access_token":"gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                          "scope":"repo,gist",
                          "token_type":"bearer"
                        }
                        """) };
                }

                throw new Exception($"Unhandled request with URI {request.RequestUri}");
            });
        var client = new HttpClient(messageHandlerMock.Object);

        AccountController controller = new(accountRepoMock.Object, client);

        // Act
        ///////

        string result = controller.GetGithubToken("test");

        // Assert
        //////////

        Assert.Equal("gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", result);
    }
}
