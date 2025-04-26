using COMP3404_Client.Services;

namespace COMP3404_Client;

public partial class App : Application
{
    readonly HttpClient m_client;
    readonly ServerService m_dataManager;

    public App()
    {
        InitializeComponent();

        m_client = new HttpClient();
        m_dataManager = new ServerService(m_client);

        MainPage = new AppShell();
    }
}
