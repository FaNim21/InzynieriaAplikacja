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
    [ObservableProperty]
    private int rokUrodzenia;

    [ObservableProperty]
    private float bmi;


    public UserProfileViewModel()
    {
        email = App.CurrentUser.Email;
        password = App.CurrentUser.Password;
        imie = App.CurrentUser.Imie;
        nazwisko = App.CurrentUser.Nazwisko;
        waga = App.CurrentUser.Waga;
        wzrost = App.CurrentUser.Wzrost;
        celKrokow = App.CurrentUser.CelKrokow;
        rokUrodzenia = App.CurrentUser.RokUrodzenia;
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        CalculateBMI();
    }

    [RelayCommand]
    public async Task SaveUserData()
    {
        try
        {
            App.CurrentUser.Email = Email;
            App.CurrentUser.Password = Password;
            App.CurrentUser.Imie = Imie;
            App.CurrentUser.Nazwisko = Nazwisko;
            App.CurrentUser.Waga = Waga;
            App.CurrentUser.Wzrost = Wzrost;
            App.CurrentUser.CelKrokow = CelKrokow;
            App.CurrentUser.RokUrodzenia = RokUrodzenia;
            App.Database.UpdateTable(App.CurrentUser);
            await Application.Current!.MainPage!.DisplayAlert("Message","Zapisano dane profilowe","Ok");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", $"blad przy zapisywaniu danych: {ex.Message}", "OK");
        }
    }

    partial void OnWzrostChanged(float value) => CalculateBMI();
    partial void OnWagaChanged(float value) => CalculateBMI();
    private void CalculateBMI()
    {
        Bmi = (float)Math.Round(Waga / ((Wzrost / 100) * (Wzrost / 100)), 2);
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
