using CommunityToolkit.Mvvm.Input;

namespace InzynieriaAplikacja.ViewModels;

public partial class MainViewModel : BaseViewModel
{
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
}
