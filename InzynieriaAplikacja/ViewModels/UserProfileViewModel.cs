using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;

namespace InzynieriaAplikacja.ViewModels;

public partial class UserProfileViewModel : BaseViewModel
{

    [ObservableProperty]
    private string email;
    [ObservableProperty]
    private string password;
    [ObservableProperty]
    private string imie;
    [ObservableProperty]
    private string nazwisko;
    [ObservableProperty]
    private float waga;
    [ObservableProperty]
    private float wzrost;
    [ObservableProperty]
    private int celKrokow;

    public UserProfileViewModel()
    {
        email = App.CurrentUser.Email;
        password = App.CurrentUser.Password;
        imie = App.CurrentUser.Imie;
        nazwisko = App.CurrentUser.Nazwisko;
        waga = App.CurrentUser.Waga;
        wzrost = App.CurrentUser.Wzrost;
        celKrokow = App.CurrentUser.CelKrokow;
    }

    [RelayCommand]
    public async Task SaveUserData()
    {
        try
        {
            App.CurrentUser.Email = email;
            App.CurrentUser.Password = password;
            App.CurrentUser.Imie = imie;
            App.CurrentUser.Nazwisko = nazwisko;
            App.CurrentUser.Waga = waga;
            App.CurrentUser.Wzrost = wzrost;
            App.CurrentUser.CelKrokow = celKrokow;
            await App.Database.UpdateUser(App.CurrentUser);
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad przy zapisywaniu danych {ex.Message}", "no dobra");
        }
    }

    [RelayCommand]
    public async Task GoBack()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///Main");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
