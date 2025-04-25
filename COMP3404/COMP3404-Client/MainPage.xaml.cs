namespace COMP3404_Client;
using COMP3404_Client.SaveLoadManagerScripts;
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void SendButtonClicked(object sender, EventArgs e)
    {
        TTS.instance.Speak(chatInputFrame.Text);
        chatInputFrame.Text = string.Empty;
    }
}
