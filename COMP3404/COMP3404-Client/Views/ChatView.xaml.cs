namespace COMP3404_Client.Views;

public partial class ChatView : ContentView
{
	public ChatView()
	{
		InitializeComponent();
	}
    private void SendButtonClicked(object sender, EventArgs e)
    {
        TTS.instance.Speak(chatInputFrame.Text);
        chatInputFrame.Text = string.Empty;
    }
}