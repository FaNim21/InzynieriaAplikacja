using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Controls;
using InzynieriaAplikacja.Models;
using System.Diagnostics;

namespace InzynieriaAplikacja.ViewModels;

public partial class LoginPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string emailText;

    [ObservableProperty]
    private string passwordText;


    public LoginPageViewModel()
    {
        emailText = "user@example.com";
        passwordText = "user123";

        //emailText = "admin";
        //passwordText = "admin";

        Task.Run(CreateAdmin);
    }

    private async Task CreateAdmin()
    {
        try
        {
            App.Database.CreateUser(new User() { Email = "admin", Password = "admin", Administrator = true });
        }
        catch { }
    }

    [RelayCommand]
    public async Task CreateAccount()
    {
        IsBusy = true;
        try
        {
            App.Database.CreateUser(new User() { Email = EmailText, Password = PasswordText });
            await Application.Current!.MainPage!.DisplayAlert("Message", $"Dodano konto użytkownika {EmailText}", "Ok");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", $"blad w resjestrowaniu {ex.Message}", "OK");
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task Login()
    {
        IsBusy = true;
        try
        {
            var user = App.Database.LoginUser(EmailText, PasswordText);

            if (user.Administrator)
            {
                await Shell.Current.GoToAsync("///Admin");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            }
            else
            {
                user.Statistic ??= new() { SpozyteKalorie = 124421 };

                App.CurrentUser = user;
                await Shell.Current.GoToAsync("///Main");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"{ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
