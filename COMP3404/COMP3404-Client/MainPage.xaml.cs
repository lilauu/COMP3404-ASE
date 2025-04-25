namespace COMP3404_Client;
using COMP3404_Shared.Models.Chats;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void SendButtonClicked(object sender, EventArgs e)
    {
        TTS.instance.Speak(chatInputFrame.Text);

        /*App.AIModel.GetResponse(chatInputFrame.Text, new Chat(), response =>
        {
            DisplayAlert("Ayy Yo Dog", response, "Sick!");
        });
        */

        string language = Preferences.Get("Language", "English");

        App.AIModel.TranslateLanguage(chatInputFrame.Text, language, new Chat(), response =>
        {
            DisplayAlert("Ayy Yo Dog, nice language", response, $"Sick!");
        });

        chatInputFrame.Text = string.Empty;

    }
}
