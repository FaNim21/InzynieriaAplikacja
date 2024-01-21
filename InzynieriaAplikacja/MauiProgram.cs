using CommunityToolkit.Maui;
using InzynieriaAplikacja.Context;
using InzynieriaAplikacja.ViewModels;
using InzynieriaAplikacja.ViewModels.Administrator;
using InzynieriaAplikacja.Views;
using Microsoft.Extensions.Logging;

namespace InzynieriaAplikacja;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<LocalDatabaseService>();

        builder.Services.AddSingleton<MainView>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<LoginView>();
        builder.Services.AddSingleton<LoginPageViewModel>();

        builder.Services.AddSingleton<AdminPanelViewModel>();
        builder.Services.AddSingleton<AdminPanelView>();

        return builder.Build();
    }
}
