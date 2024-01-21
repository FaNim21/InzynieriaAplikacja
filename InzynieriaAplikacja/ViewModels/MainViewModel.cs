using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using MongoDB.Bson;
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
    private ObservableCollection<Training> trainings = new();


    public MainViewModel()
    {
        /*var config = new RealmConfiguration(); // Default configuration
        Realm.DeleteRealm(config); // Delete the Realm file
        Realm = Realm.GetInstance(config); // Recreate the Realm*/

        Realm = Realm.GetInstance();
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
    public async Task GetTraining()
    {
        IsBusy = true;
        /*Sync = new FlexibleSyncConfiguration(App.RealmApp.CurrentUser!)
        {
            PopulateInitialSubscriptions = (realm) =>
            {
                var myItems = realm.All<Training>();
                realm.Subscriptions.Add(myItems);
            }
        };
        Trace.WriteLine(Sync.PopulateInitialSubscriptions.ToString());
        Realm = await Realm.GetInstanceAsync(Sync);
        Trace.WriteLine("asdasd");*/

        trainings.Clear();
        var _trainings = Realm.All<Training>();
        foreach (Training training in _trainings)
        {
            trainings.Add(training); 
            //Trace.WriteLine($"{training.Name}");
        }

        Trace.WriteLine(trainings.Count);

        //await Application.Current.MainPage.DisplayAlert(Realm.All<Training>().ToString(), "", "OK");
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
            });
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
        }
        IsBusy = false;
    }

    [RelayCommand]
    async Task DeleteTraining()
    {
        IsBusy = true;
        try
        {
            Training lastTraining = trainings.LastOrDefault();

            Realm.Write(() =>
            {
                Realm.Remove(lastTraining);
            });

            trainings.Remove(lastTraining);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
        }
        IsBusy = false;
    }
}
