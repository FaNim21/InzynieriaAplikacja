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
        emailText = "asdasd@wp.pl";
        passwordText = "1432432a";

        Task.Run(CreateAdmin);
    }

    private async Task CreateAdmin()
    {
        try
        {
            await App.Database.CreateUser(new User() { Email = "admin", Password = "admin", Administrator = true });
        }
        catch { }
    }

    public static async Task StartDashboard()
    {
        await Shell.Current.GoToAsync("///Main");
    }

    [RelayCommand]
    public async Task CreateAccount()
    {
        IsBusy = true;
        try
        {
            var users = await App.Database.GetUsers();
            foreach (var user in users)
            {
                Trace.Write(user.Email);
            }

            await App.Database.CreateUser(new User() { Email = EmailText, Password = PasswordText });
        }
        catch (Exception ex)
        {
            await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w resjestrowaniu {ex.Message}", "no dobra");
        }   
        IsBusy = false;
    }

    [RelayCommand]
    public async Task Login()
    {
        IsBusy = true;
        try
        {
            var user = await App.Database.LoginUser(EmailText, PasswordText);

            if (user.Administrator)
            {
                await Shell.Current.GoToAsync("///Admin");
            }
            else
            {
                App.CurrentUser = user;
                await Shell.Current.GoToAsync("///Main");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }
        catch (Exception ex)
        {
            await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w logowaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
