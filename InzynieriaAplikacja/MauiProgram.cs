using CommunityToolkit.Maui;
using InzynieriaAplikacja.ViewModels;
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

        builder.Services.AddSingleton<MainView>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<LoginView>();
        builder.Services.AddSingleton<LoginPageViewModel>();

        builder.Services.AddSingleton<TrainingView>();
        builder.Services.AddSingleton<TrainingViewModel>();

        return builder.Build();
    }

    //HRT60TQ2nOMfxrX0
    //sznsmcvviwGaHILc
    //eI8c8jBqmzOOtIWY
    //onbhtr7Ryna7TPex
}
