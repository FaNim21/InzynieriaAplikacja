using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using Realms;
using Realms.Sync;

namespace InzynieriaAplikacja.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public Realm Realm { get; set; }
    public FlexibleSyncConfiguration Sync { get; set; }

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
        Sync = new FlexibleSyncConfiguration(App.RealmApp.CurrentUser!);
        Realm = await Realm.GetInstanceAsync(Sync);
        //var users = Realm.All<User>();

        await Application.Current.MainPage.DisplayAlert(Realm.All<Training>().ToString(), "", "OK");
    }
}
