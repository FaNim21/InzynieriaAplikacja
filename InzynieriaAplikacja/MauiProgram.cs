using CommunityToolkit.Maui;
using InzynieriaAplikacja.Context;
using InzynieriaAplikacja.ViewModels;
using InzynieriaAplikacja.ViewModels.Administrator;
using InzynieriaAplikacja.Views;
using InzynieriaAplikacja.Views.AdminViews;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Pedometer;

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

        builder.Services.AddSingleton<TrainingView>();
        builder.Services.AddSingleton<TrainingViewModel>();

        builder.Services.AddSingleton<AdminControlUserView>();
        builder.Services.AddSingleton<AdminControlUserViewModel>();

        builder.Services.AddSingleton<AdminControlTrainingView>();
        builder.Services.AddSingleton<AdminControlTrainingViewModel>();

        builder.Services.AddSingleton<UserProfileView>();
        builder.Services.AddSingleton<UserProfileViewModel>();

        builder.Services.AddSingleton<ActivityView>();
        builder.Services.AddSingleton<ActivityViewModel>();

        builder.Services.AddSingleton<MealView>();
        builder.Services.AddSingleton<MealViewModel>();

        builder.Services.AddSingleton(Pedometer.Default);

        return builder.Build();
    }
}
