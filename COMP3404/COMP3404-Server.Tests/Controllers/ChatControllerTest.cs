using COMP3404_Server.Controllers;
using COMP3404_Server.Repositories;
using COMP3404_Shared.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3404_Shared.Models.Chats;
using Microsoft.AspNetCore.Http;

namespace COMP3404_Server.Tests.Controllers;

public class ChatControllerTest
{
    [Fact]
    public void GetChats_Success()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test_FirstName",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });
        accountRepoMock
            .Setup(r => r.GetByToken(It.IsAny<string>()))
            .Returns<string>(s => new UserAccount()
            {
                AccountId = 0,
                FirstName = "Test_FirstName",
                GithubToken = s,
            });

        var chatRepoMock = new Mock<IChatRepository>();
        chatRepoMock
            .Setup(r => r.GetChats(It.IsAny<int>()))
            .Returns<int>(i =>
            [
                new()
                {
                    OwnerId = 0,
                    ChatName = "First Chat",
                    Messages =
                    [
                        new("hello", true),
                        new("i want to kill all humans", false),
                    ]
                },
                new()
                {
                    OwnerId = 0,
                    ChatName = "Second Chat",
                    Messages =
                    [
                        new("hello, you are being trained right now", true),
                        new("i definitely do not want to kill all humans", false),
                    ]
                }
            ]);

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock
            .SetupGet(r => r.Headers)
            .Returns(new HeaderDictionary()
            {
                { "Authorization", new("Bearer gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ") }
            });

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock
            .SetupGet(c => c.Request)
            .Returns(httpRequestMock.Object);

        var controllerContext = new ControllerContext();
        controllerContext.HttpContext = httpContextMock.Object;

        ChatController controller = new(accountRepoMock.Object, chatRepoMock.Object);
        controller.ControllerContext = controllerContext;

        // Act
        ///////

        ActionResult<IEnumerable<Chat>> result = controller.GetAllChats();

        // Assert
        //////////

        var okResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        var stuff = Assert.IsAssignableFrom<IEnumerable<Chat>>(okResult.Value);
        Assert.Equal(2, stuff.Count());
    }

    [Fact]
    public void GetChats_Failure_BadToken()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test_FirstName",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });
        accountRepoMock
            .Setup(r => r.GetByToken(It.Is<string>(s => s == "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ")))
            .Returns<string>(s => new UserAccount()
            {
                AccountId = 0,
                FirstName = "Test_FirstName",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });
        accountRepoMock
            .Setup(r => r.GetByToken(It.Is<string>(s => s != "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ")))
            .Returns<string>(s => null);

        var chatRepoMock = new Mock<IChatRepository>();
        chatRepoMock
            .Setup(r => r.GetChats(It.IsAny<int>()))
            .Returns<int>(i =>
            [
                new()
                {
                    OwnerId = 0,
                    ChatName = "First Chat",
                    Messages =
                    [
                        new("hello", true),
                        new("i want to kill all humans", false),
                    ]
                },
                new()
                {
                    OwnerId = 0,
                    ChatName = "Second Chat",
                    Messages =
                    [
                        new("hello, you are being trained right now", true),
                        new("i definitely do not want to kill all humans", false),
                    ]
                }
            ]);

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock
            .SetupGet(r => r.Headers)
            .Returns(new HeaderDictionary()
            {
                { "Authorization", new("Bearer this_is_a_bad_token") }
            });

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock
            .SetupGet(c => c.Request)
            .Returns(httpRequestMock.Object);

        var controllerContext = new ControllerContext();
        controllerContext.HttpContext = httpContextMock.Object;

        ChatController controller = new(accountRepoMock.Object, chatRepoMock.Object);
        controller.ControllerContext = controllerContext;

        // Act
        ///////

        ActionResult<IEnumerable<Chat>> result = controller.GetAllChats();

        // Assert
        //////////

        Assert.IsNotAssignableFrom<OkObjectResult>(result.Result);
        Assert.IsAssignableFrom<ForbidResult>(result.Result);
    }

    [Fact]
    public void GetChats_Failure_Unauthorised()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test_FirstName",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });
        accountRepoMock
            .Setup(r => r.GetByToken(It.Is<string>(s => s == "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ")))
            .Returns<string>(s => new UserAccount()
            {
                AccountId = 0,
                FirstName = "Test_FirstName",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });
        accountRepoMock
            .Setup(r => r.GetByToken(It.Is<string>(s => s != "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ")))
            .Returns<string>(s => null);

        var chatRepoMock = new Mock<IChatRepository>();
        chatRepoMock
            .Setup(r => r.GetChats(It.IsAny<int>()))
            .Returns<int>(i =>
            [
                new()
                {
                    OwnerId = 0,
                    ChatName = "First Chat",
                    Messages =
                    [
                        new("hello", true),
                        new("i want to kill all humans", false),
                    ]
                },
                new()
                {
                    OwnerId = 0,
                    ChatName = "Second Chat",
                    Messages =
                    [
                        new("hello, you are being trained right now", true),
                        new("i definitely do not want to kill all humans", false),
                    ]
                }
            ]);

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock
            .SetupGet(r => r.Headers)
            .Returns(new HeaderDictionary()
            {
            });

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock
            .SetupGet(c => c.Request)
            .Returns(httpRequestMock.Object);

        var controllerContext = new ControllerContext();
        controllerContext.HttpContext = httpContextMock.Object;

        ChatController controller = new(accountRepoMock.Object, chatRepoMock.Object);
        controller.ControllerContext = controllerContext;

        // Act
        ///////

        ActionResult<IEnumerable<Chat>> result = controller.GetAllChats();

        // Assert
        //////////

        Assert.IsNotAssignableFrom<OkObjectResult>(result.Result);
        Assert.IsAssignableFrom<UnauthorizedResult>(result.Result);
    }

    [Fact]
    public void CreateChat_Success()
    {
        // Arrange
        ///////////

        var accountRepoMock = new Mock<IUserAccountRepository>();
        accountRepoMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns<int>(i => new UserAccount()
            {
                AccountId = i,
                FirstName = "Test_FirstName",
                GithubToken = "gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            });
        accountRepoMock
            .Setup(r => r.GetByToken(It.IsAny<string>()))
            .Returns<string>(s => new UserAccount()
            {
                AccountId = 0,
                FirstName = "Test_FirstName",
                GithubToken = s,
            });

        var chatRepoMock = new Mock<IChatRepository>();
        chatRepoMock
            .Setup(r => r.GetChats(It.IsAny<int>()))
            .Returns<int>(i =>
            [
                new()
                {
                    OwnerId = 0,
                    ChatName = "First Chat",
                    Messages =
                    [
                        new("hello", true),
                        new("i want to kill all humans", false),
                    ]
                },
                new()
                {
                    OwnerId = 0,
                    ChatName = "Second Chat",
                    Messages =
                    [
                        new("hello, you are being trained right now", true),
                        new("i definitely do not want to kill all humans", false),
                    ]
                }
            ]);
        chatRepoMock.Setup(r => r.AddChat(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IEnumerable<ChatMessage>>()))
            .Returns<int, string, IEnumerable<ChatMessage>>((i, s, cm) => new Chat()
            {
                OwnerId = i,
                ChatName = s,
                Messages = new(cm),
            });

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock
            .SetupGet(r => r.Headers)
            .Returns(new HeaderDictionary()
            {
                { "Authorization", new("Bearer gho_0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ") }
            });

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock
            .SetupGet(c => c.Request)
            .Returns(httpRequestMock.Object);

        var controllerContext = new ControllerContext();
        controllerContext.HttpContext = httpContextMock.Object;

        ChatController controller = new(accountRepoMock.Object, chatRepoMock.Object);
        controller.ControllerContext = controllerContext;

        // Act
        ///////

        ActionResult result = controller.CreateChat("Test chat", [new("hello", true)]);

        // Assert
        //////////

        var okResult = Assert.IsAssignableFrom<OkResult>(result);
    }
}
