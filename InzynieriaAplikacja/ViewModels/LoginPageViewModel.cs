using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        /*emailText = "asdasd@wp.pl";
        passwordText = "1432432a";*/

        emailText = "admin";
        passwordText = "admin";

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
            }
            else
            {
                user.Statistic ??= new() { SpozyteKalorie = 124421 };
                user.Statistic.Trainings.Add(new Training() { Name = "asdasd" });
                user.Statistic.Activities.Add(new Models.Activity() { Kroki = 42069 });
                user.Statistic.EatenMeals.Add(new Models.Meal() { Name = "Kotlet z ziemniaczkami" });

                //EatenMeal eaten = new() { User = user, Meal = new() { Name = "ASDASDRGSGR ASDASD" } };
                //App.Database.CreateTable(eaten);

                //Models.Activity Activity = new() { AktywnosciDodatkowe = "lol aktywnosc" };
                //App.Database.CreateTable(Activity);
                //user.Statistic.Activities.Add(Activity);

                App.CurrentUser = user;
                await Shell.Current.GoToAsync("///Main");
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"{ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
