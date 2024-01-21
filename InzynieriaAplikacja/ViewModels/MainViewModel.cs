using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Realms;
using Realms.Sync;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace InzynieriaAplikacja.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public Realm Realm { get; set; }
    public FlexibleSyncConfiguration? Sync { get; set; }

    [ObservableProperty]
    private ObservableCollection<Training>? trainings;

    [ObservableProperty]
    private float step;

    [ObservableProperty]
    private int currentStep;

    private int maxStep=100;


    public MainViewModel()
    {
        Trainings = [];
        /*var config = new RealmConfiguration(); // Default configuration
        Realm.DeleteRealm(config); // Delete the Realm file
        Realm = Realm.GetInstance(config); // Recreate the Realm*/

        Realm = Realm.GetInstance();

        var _trainings = Realm.All<Training>();
        foreach (Training training in _trainings)
        {
            Trainings.Add(training);
            //Trace.WriteLine($"{training.Name}");
        }
    }

    [RelayCommand]
    public async Task SignOut()
    {
        IsBusy = true;
        try
        {
            var currentuserId = App.RealmApp.CurrentUser.Id;
            await App.RealmApp.RemoveUserAsync(App.RealmApp.CurrentUser);
            var noMoreCurrentUser = App.RealmApp.AllUsers.FirstOrDefault(u => u.Id == currentuserId);
            await Shell.Current.GoToAsync("///Login");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
        }
        IsBusy = false;
    }


    [RelayCommand]
    public async Task AddTraining()
    {
        IsBusy = true;
        try
        {
            Realm.Write(() =>
            {
                var todo = new Training
                {
                    Name = "asdasd123123213",
                    Time = DateTime.UtcNow.ToString(),
                    SpaloneKalorie = 1000,
                    Cwiczenia = "eloo"
                };
                Realm.Add(todo);
                Trainings!.Add(todo);
            });
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayPromptAsync("Error", ex.Message);
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task DeleteTraining()
    {
        if (Trainings == null) return;

        IsBusy = true;
        try
        {
            Training lastTraining = Trainings.LastOrDefault()!;

            Realm.Write(() =>
            {
                Realm.Remove(lastTraining);
            });

            Trainings.Remove(lastTraining);
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayPromptAsync("Error", ex.Message);
        }
        IsBusy = false;
    }

    [RelayCommand]
    public void MakeStep()
    {
        CurrentStep++;
        Step = (float) CurrentStep / maxStep;
    }

    [RelayCommand]
    public async Task GoToTraining()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///Training");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
        IsBusy = false;
    }
}
