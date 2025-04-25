namespace COMP3404_Client.Views;

public partial class ChatView : ContentView
{
    public static readonly BindableProperty ChatNameProperty =
        BindableProperty.Create(nameof(ChatName), typeof(string), typeof(MessageView), string.Empty);
    public string ChatName
    {
        get => (string)GetValue(ChatNameProperty);
        set => SetValue(ChatNameProperty, value);
    }

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