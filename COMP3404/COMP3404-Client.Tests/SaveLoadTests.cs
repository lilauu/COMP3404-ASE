namespace COMP3404_Client.Tests;
using COMP3404_Client.SaveLoadManagerScripts;

    public class SaveLoadTests
    {
        [Fact]
        public void SaveDataToFile_Success()
        {
            //Arrange
            SaveLoadManager saveLoadManager = new ();
            saveLoadManager.DeleteFileIfExists("TestData.txt");

            //Act 
            List<string> testData = ["Test One", "Test Two", "Test Three"];

            saveLoadManager.SaveDataToFile(testData, "TestData.txt");

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "COMP3404");
            path = Path.Combine(path, "TestData.txt");

            //Assert
            Assert.True(File.Exists(path));

            saveLoadManager.DeleteFileIfExists("TestData.txt");
        }

        [Fact]
        public void LoadDataFromFile_Success()
        {
            //Arrange
            SaveLoadManager saveLoadManager = new ();
            saveLoadManager.DeleteFileIfExists("TestData.txt");

            //Act 
            List<string> testData = ["Test One", "Test Two", "Test Three"];

            saveLoadManager.SaveDataToFile(testData, "TestData.txt");

            var loadList = saveLoadManager.LoadDataFromFile<List<string>>("TestData.txt");

            //Assert
            Assert.Equal(testData, loadList);

            saveLoadManager.DeleteFileIfExists("TestData.txt");
        }

    [Fact]
    public void LoadDataFromFile_Failure()
    {
        //Arrange
        SaveLoadManager saveLoadManager = new();
        saveLoadManager.DeleteFileIfExists("TestData.txt");

        //Act 

        var loadList = () => saveLoadManager.LoadDataFromFile<List<string>>("DONOTRENAMETHIS.txt");

        //Assert
        Assert.Throws<FileNotFoundException>(loadList);
    }


}