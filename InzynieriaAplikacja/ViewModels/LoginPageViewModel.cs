using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;

namespace InzynieriaAplikacja.ViewModels;

public partial class LoginPageViewModel : BaseViewModel
{
    private MainViewModel mainViewModel;

    [ObservableProperty]
    private string emailText;

    [ObservableProperty]
    private string passwordText;


    public LoginPageViewModel(MainViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;

        /*emailText = "user@example.com";
        passwordText = "user123";*/

        emailText = "admin";
        passwordText = "admin";

        CreateAdmin();
    }

    private static void CreateAdmin()
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
            App.Database.CreateUser(new User() { Email = EmailText, Password = PasswordText, CelKrokow = 1000 });
            await Application.Current!.MainPage!.DisplayAlert("Message", $"Dodano konto użytkownika {EmailText}", "Ok");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", $"Błąd w rejestrowaniu admin: {ex.Message}", "OK");
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
                Statistic statistic;
                if (user.IdStatistic == 0)
                {
                    statistic = new() { };
                    App.Database.CreateTable(statistic);
                    user.IdStatistic = statistic.Id;
                    App.Database.UpdateTable(user);
                }
                else
                {
                    statistic = App.Database.GetTable<Statistic>().Where(x => x.Id == user.IdStatistic).First()!;
                }
                App.CurrentStatistics = statistic!;
                App.CurrentUser = user;
                mainViewModel.OnLoad();
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
