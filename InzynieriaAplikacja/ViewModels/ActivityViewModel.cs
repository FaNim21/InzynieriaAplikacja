using CommunityToolkit.Mvvm.ComponentModel;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels;

public partial class ActivityViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Activity> activities = [];

    [ObservableProperty]
    private ObservableCollection<Activity> currentActivity = [];


    public ActivityViewModel() { }

    public override void OnAppearing()
    {
        Task.Run(GetActivities);
    }

    public async Task GetActivities()
    {
        try
        {
            CurrentActivity.Clear();
            CurrentActivity.Add(App.CurrentActivity);

            Activities.Clear();
            Activities = new(App.Database.GetTable<Activity>().Where(x => x.StatisticId == App.CurrentStatistics.Id && x.Id != currentActivity[0].Id));
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("ERROR", $"Błąd przy pobieraniu aktywności: {ex.Message}", "OK");
        }
    }

}
