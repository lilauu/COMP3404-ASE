namespace COMP3404_Client.Tests;
using COMP3404_Client.SaveLoadManager;
{
    public class SaveLoadTests
    {
        [Fact]
        public void SaveFileExists()
        {
            //Arrange
            SaveLoadManager saveLoadManager = new SaveLoadManager();
            saveLoadManager.DeleteFileIfExists("TestData.txt");

            //Act 
            List<string> testData = ["Test One", "Test Two", "Test Three"];

            saveLoadManager.SaveDataToFile(testData, "TestData.txt");

            //Assert
            Assert.True(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestData.txt")));
        }

        [Fact]
        public void LoadingFileWorks()
        {
            //Arrange
            SaveLoadManager saveLoadManager = new SaveLoadManager();
            saveLoadManager.DeleteFileIfExists("TestData.txt");

            //Act 
            List<string> testData = ["Test One", "Test Two", "Test Three"];

            saveLoadManager.SaveDataToFile(testData, "TestData.txt");

            var loadList = saveLoadManager.LoadDataFromFile<List<string>>("TestData.txt");

            //Assert
            Assert.Equal(testData, loadList);
        }
    }
    
}
