namespace COMP3404_Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        TTS tts = new();

        MainPage = new AppShell();
    }
}
