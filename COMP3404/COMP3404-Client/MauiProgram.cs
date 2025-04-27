using COMP3404_Client.Services;
using COMP3404_Client.Services.AI;
using Microsoft.Extensions.Logging;
using COMP3404_Client.Services.Storage;
using Microsoft.Extensions.Configuration;

namespace COMP3404_Client;

public static class MauiProgram
{
    static IServiceProvider m_serviceProvider = null!;
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

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
    {
        builder.Services
            .AddSingleton<IAIModelService, GeminiAIModelService>() // todo: replace with actual service once implemented
            .AddSingleton<HttpClient>()
            .AddSingleton<ServerService>()
            .AddTransient<ServerStorageService>()
            .AddTransient<DiskStorageService>()
            .AddSingleton<TTSService>()
            ;

        return builder;
    }

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
