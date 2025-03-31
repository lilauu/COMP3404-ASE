﻿using Microsoft.Extensions.Logging;
#if WINDOWS
using WinUIEx;
#endif

namespace COMP3404_Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        if (OperatingSystem.IsWindows())
        {
            WinUIEx.WebAuthenticator.CheckOAuthRedirectionActivation();
        }

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
