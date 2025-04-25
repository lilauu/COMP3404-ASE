using COMP3404_Client.API;

namespace COMP3404_Client;

public partial class App : Application
{
    readonly HttpClient m_client;
    readonly DataManager m_dataManager;
    public App()
    {
        InitializeComponent();

        TTS tts = new();
        m_client = new HttpClient();
        m_dataManager = new DataManager(m_client);

        MainPage = new AppShell();
    }
}
