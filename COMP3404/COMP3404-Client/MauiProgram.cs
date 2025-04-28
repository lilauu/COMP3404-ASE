using COMP3404_Client.Services;
using COMP3404_Client.Services.AI;
using COMP3404_Client.Services.Storage;
using Microsoft.Extensions.Logging;

namespace COMP3404_Client;

public static class MauiProgram
{
    static IServiceProvider m_serviceProvider = null!;
    /// <summary>
    /// Helper function for resolving service dependencies without automatic constructor dependency injection.
    /// </summary> 
    public static TService GetService<TService>()
        => m_serviceProvider.GetService<TService>();

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        m_serviceProvider = app.Services;
        return app;
    }

    /// <summary>
    /// Extension method. Registers the services used by the application.
    /// </summary>
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
    {
        builder.Services
            .AddSingleton<IAIModelService, GeminiAIModelService>()
            .AddSingleton<HttpClient>()
            .AddSingleton<ServerService>()
            .AddTransient<ServerStorageService>()
            .AddTransient<DiskStorageService>()
            .AddSingleton<TTSService>()
            .AddSingleton(Preferences.Default)
            ;

        return builder;
    }

    /// <summary>
    /// Extension method. Registers the ViewModels used by the application as services.
    /// </summary>
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services
            .AddSingleton<ViewModels.MainPageViewModel>()
            .AddSingleton<ViewModels.SettingsPageViewModel>()
            .AddSingleton<ViewModels.HistoryPageViewModel>()
            ;

        builder.Services
            .AddTransient<ViewModels.MessageViewModel>()
            .AddTransient<ViewModels.ChatViewModel>()
            ;

        return builder;
    }

    /// <summary>
    /// Extension method. Registers the Views used by the application as services.
    /// </summary>
    public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services
            .AddSingleton<MainPage>()
            .AddSingleton<SettingsPage>()
            .AddSingleton<HistoryPage>()
            .AddSingleton<Views.NavbarView>()
            ;

        return builder;
    }
}
