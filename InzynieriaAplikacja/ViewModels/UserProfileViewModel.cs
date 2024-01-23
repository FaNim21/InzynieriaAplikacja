using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

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
            App.Database.UpdateTable(App.CurrentUser);
            await Application.Current!.MainPage!.DisplayAlert("Message","Zapisano dane profilowe","Ok");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad przy zapisywaniu danych {ex.Message}", "no dobra");
        }
    }

    [RelayCommand]
    public async Task DeleteProfile()
    {
        IsBusy = true;
        try
        {
            bool result = await Application.Current!.MainPage!.DisplayAlert("Question", "Czy na pewno chcesz usunąć swój profil?", "Nie", "Tak");
            if (!result)
            {
                string pass = await Application.Current!.MainPage!.DisplayPromptAsync("Question2", "Aby potwierdzić usunięcie podaj hasło");
                if (pass == App.CurrentUser.Password)
                {
                    App.Database.DeleteTable(App.CurrentUser);
                    await Shell.Current.GoToAsync("///Login");
                    await Application.Current!.MainPage!.DisplayAlert("Message", $"Konto użytkownika {App.CurrentUser.Email} zostało usunięte", "Ok");
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Message", "Nieprawidłowe hasło", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"Blad przy usuwaniu: {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
