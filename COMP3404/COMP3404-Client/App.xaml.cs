using COMP3404_Client.AI;
using COMP3404_Client.AI.Stub;

namespace COMP3404_Client;

public partial class App : Application
{
    public static IAIModel AIModel { get; private set; }
    public App()
    {
        InitializeComponent();

        TTS tts = new();
        AccountManager accountManager = new();

        AIModel ??= new StubAIModel();

        MainPage = new AppShell();
    }
}
