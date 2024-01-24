using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using Plugin.Maui.Pedometer;
using System.Diagnostics;

namespace InzynieriaAplikacja.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private IPedometer pedometer;

    [ObservableProperty]
    private int step;

    [ObservableProperty]
    private int currentStep;

    [ObservableProperty]
    private int maxStep;

    [ObservableProperty]
    private float distance;

    [ObservableProperty]
    private int calories;

    public MainViewModel(IPedometer pedometer)
    {
        this.pedometer = pedometer;
    }

    public void OnLoad()
    {
        Step = 0;
        CurrentStep = 0;
        Distance = 0;
        Calories = 0;
        MaxStep = App.CurrentUser.CelKrokow;
        StartCounting();

        try
        {
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            Models.Activity? todayActivity = App.Database.GetTable<Models.Activity>().Where(x => x.StatisticId == App.CurrentStatistics.Id && x.Date == today).FirstOrDefault();
            if (todayActivity == null)
            {
                todayActivity = new() { Date = today, StatisticId = App.CurrentStatistics.Id };
                App.Database.CreateTable(todayActivity);
            }
            else
            {
                CurrentStep = todayActivity.Kroki;
                Distance = todayActivity.PokonanyDystans;
                Calories = todayActivity.SpaloneKalorie;
            }
            App.CurrentActivity = todayActivity;
        }
        catch (Exception ex)
        {
            Application.Current!.MainPage!.DisplayAlert("Message", $"{ex.Message}", "Ok");
        }
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        MaxStep = App.CurrentUser.CelKrokow;
        if (string.IsNullOrEmpty(App.CurrentUser.Nazwisko) || App.CurrentUser.Wzrost == 0 || App.CurrentUser.CelKrokow == 0 || App.CurrentUser.Waga == 0 || string.IsNullOrEmpty(App.CurrentUser.Imie) || App.CurrentUser.RokUrodzenia == 0)
        {
            Application.Current!.MainPage!.DisplayAlert("Message", "Uzupełnij dane profilowe w zakładce Profil!", "Ok");
        }
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        App.CurrentActivity.Kroki = CurrentStep;
        App.CurrentActivity.PokonanyDystans = Distance;
        App.CurrentActivity.SpaloneKalorie = Calories;
        App.Database.UpdateTable(App.CurrentActivity);
    }

    partial void OnCurrentStepChanged(int value)
    {
        Step = (int)Math.Round(((float)value / MaxStep) * 100);
        Distance = (float)Math.Round((value * 0.7f) / 1000, 2);
        Calories = (int)Math.Round(value * 0.04f);
    }

    public void StartCounting()
    {
        pedometer.ReadingChanged += (sender, reading) =>
        {
            CurrentStep = reading.NumberOfSteps;
            Step = CurrentStep / MaxStep;
        };
        try
        {
            pedometer.Start();
        }
        catch (Exception ex)
        {
            Application.Current!.MainPage!.DisplayAlert("Error", ex.Message, ":(");
        }
    }
}
