
using COMP3404_Client.Services;

namespace COMP3404_Client.Views;

public partial class ChatView : ContentView
{
	public static readonly BindableProperty ChatNameProperty =
		BindableProperty.Create(nameof(ChatName), typeof(string), typeof(ChatView), string.Empty);
	public string ChatName
	{
		get => (string)GetValue(ChatNameProperty);
		set => SetValue(ChatNameProperty, value);
	}

	private TTSService TTS;

	public ChatView()
		: this(MauiProgram.GetService<TTSService>())
	{ }

	public ChatView(TTSService tts)
	{
		TTS = tts;
		InitializeComponent();
	}

	private bool isWaiting = false;
	private async void TextEntered(object sender, EventArgs e)
	{
		if (isWaiting)
			return;
		TTS.Speak(chatInputFrame.Text);

		// This is a stupid hack that I hoped that I would never have to write in my career
		// but the *enlightened* individuals over at Microsoft in all their wisdom decided that
		// the order of execution for an Entry should be Completed -> ReturnCommand but at the same
		// time the order of execution for a Button should be Command -> Clicked. This inconsistency
		// is stupid, but the *REAL* problem is the fact that the Completed event fires before the
		// ReturnCommand on an Entry. This is *extremely* detrimental to MVVM, as there is no way to do
		// View-only behaviour on these controls, like... I don't know... clearing a FUCKING TEXT BOX
		// *after* doing the ViewModel behaviour controlled by the ReturnCommand. The text is already
		// long gone and you're left on your ReturnCommand pissing in the wind without any fucking data.
		// Fantastic. Wonderful. Thank you Microsoft, very cool.
		//
		// As a workaround, I have made this event wait for 0.1 seconds before clearing the text, and
		// added some basic debounce as a coping mechanism. Just what I needed late at night with a deadline
		// in like 3 days and like 10 github issues left to close.
		isWaiting = true;
		await Task.Delay(100);
		chatInputFrame.Text = string.Empty;
		isWaiting = false;
	}
}