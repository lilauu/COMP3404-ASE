using COMP3404_Client.Services.Storage;
using COMP3404_Shared.Models.Chats;

namespace COMP3404_Client.Tests;

public class SaveLoadTests
{
    public static readonly Chat firstTestChat = new() { ChatName = "FirstTestChat", Messages = [new("Test One", true), new("Test Two", false), new("Test Three", true)] };

    [Fact]
    public void Disk_SaveChat_Success()
    {
        //Arrange
        DiskStorageService saveLoadManager = new ();

        //Act 
        saveLoadManager.SaveChat(firstTestChat);

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        path = Path.Combine(path, "COMP3404");
        path = Path.Combine(path, $"{firstTestChat.ChatName}.json");

        //Assert
        Assert.True(File.Exists(path));
    }

    [Fact]
    public async void Disk_LoadChats_Success()
    {
        //Arrange
        DiskStorageService saveLoadManager = new();

        //Act 
        saveLoadManager.SaveChat(firstTestChat);

        var result = await saveLoadManager.LoadChatsAsync();
        var firstChat = result.FirstOrDefault();

        //Assert
        Assert.Single(result);
        Assert.NotNull(firstChat);
        Assert.Equal(firstTestChat.ChatName, firstChat.ChatName);
        Assert.Equal(3, firstChat.Messages.Count);
        Assert.Equal(firstTestChat.Messages.Count, firstChat.Messages.Count);
        for (int i = 0; i < firstChat.Messages.Count; i++)
        {
            Assert.Equal(firstTestChat.Messages[i].Message, firstChat.Messages[i].Message);
            Assert.Equal(firstTestChat.Messages[i].Id, firstChat.Messages[i].Id);
            Assert.Equal(firstTestChat.Messages[i].Timestamp, firstChat.Messages[i].Timestamp);
            Assert.Equal(firstTestChat.Messages[i].IsHumanSender, firstChat.Messages[i].IsHumanSender);
        }
    }
}