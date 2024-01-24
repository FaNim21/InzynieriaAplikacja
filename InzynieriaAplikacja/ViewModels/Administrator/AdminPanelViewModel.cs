using CommunityToolkit.Mvvm.Input;

namespace InzynieriaAplikacja.ViewModels.Administrator;

public partial class AdminPanelViewModel : BaseViewModel
{
    public AdminPanelViewModel()
    {

    }

    [RelayCommand]
    public async Task OpenUserControl()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///AdminUserControl");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task OpenTrainingControl()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///AdminTrainingControl");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task OpenMealControl()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///AdminMealControl");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task SignOut()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///Login");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
