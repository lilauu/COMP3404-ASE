using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3404_Client.Services;
using COMP3404_Client.Services.AI;
using COMP3404_Client.Services.Storage;
using COMP3404_Client.ViewModels;
using COMP3404_Shared.Models.Api;
using COMP3404_Shared.Models.Chats;
using Moq;
using Moq.Protected;

namespace COMP3404_Client.Tests;

public class ChatViewModelTests
{
    [Fact]
    public void ChatViewModel_Integration()
    {
        // Arrange
        ///////////

        // Mocking this despite being an integration test because
        // preferences arent available in a testing environment
        var preferencesMock = new Mock<IPreferences>();
        preferencesMock
            .Setup(p => p.Get(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>()))
            .Returns<string, bool, string>((s, t, sn) => t);
        preferencesMock
            .Setup(p => p.Get(It.IsAny<string>(), It.IsAny<float>(), It.IsAny<string>()))
            .Returns<string, float, string>((s, t, sn) => t);

        // mocking this despite this being an integration test because
        // the server won't be running here so itll just always be timing out
        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns((HttpRequestMessage request, CancellationToken token) =>
            {
                var requestUri = request.RequestUri?.ToString();
                if (requestUri?.StartsWith("https://github.com/login/oauth/access_token") == true)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent("s_sampleAuthResponse") };
                }

                throw new Exception($"Unhandled request with URI {request.RequestUri}");
            });

        var httpClient = new HttpClient(messageHandlerMock.Object);
        var serverService = new ServerService(httpClient);

        // using the stub here instead of the real thing
        // because I don't want to waste API points
        var aiModel = new StubAIModelService(serverService);

        var diskStorage = new DiskStorageService();
        var serverStorage = new ServerStorageService(serverService);
        var tts = new TTSService(preferencesMock.Object);


        Chat chat = new() { ChatName = "Test Chat" };
        ChatViewModel viewModel = new(chat, aiModel, diskStorage, serverStorage, tts);

        // Act
        ///////

        string message = "This is a test message";
        viewModel.SendChatMessage.Execute(message);

        // Assert
        //////////

        // should have 2 messages, the prompt and the response
        Assert.Equal(2, viewModel.Messages.Count);
        Assert.Equal(message, viewModel.Messages[0].Message);
        Assert.True(viewModel.Messages[0].IsSender);
        Assert.False(viewModel.Messages[1].IsSender);
        
    }
}
