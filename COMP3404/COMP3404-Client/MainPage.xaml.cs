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
        chatInputFrame.Text = string.Empty;
    }

    private void SaveButtonClicked(object sender, EventArgs e)
    {
        
    }
}
