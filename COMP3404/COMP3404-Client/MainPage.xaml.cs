namespace COMP3404_Client;
using COMP3404_Client.SaveLoadManagerScripts;
public partial class MainPage : ContentPage
{
    SaveLoadManager saveLoadManager;
    public MainPage()
    {
        InitializeComponent();
        saveLoadManager = new ();
    }

    private void SendButtonClicked(object sender, EventArgs e)
    {
        TTS.instance.Speak(chatInputFrame.Text);
        //TestThing.Message = chatInputFrame.Text;
    }

    private void SaveButtonClicked(object sender, EventArgs e)
    {   
        List<String> list = ["One", "Two", "Three", "Four"];
        saveLoadManager.SaveDataToFile(list, "ChatLogsTest.txt");
    }
}
