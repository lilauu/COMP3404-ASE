namespace COMP3404_Client;

/// <summary>
/// The root of the MAUI application
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Constructor for <see cref="App"/>
    /// </summary>
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
