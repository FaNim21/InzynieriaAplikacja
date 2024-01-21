using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Controls;
using InzynieriaAplikacja.Models;

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
