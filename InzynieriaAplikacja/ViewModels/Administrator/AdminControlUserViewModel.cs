using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Controls;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels.Administrator;

public partial class AdminControlUserViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<User> users = [];


    public AdminControlUserViewModel()
    {
        
    }

    public override void OnAppearing()
    {
        Task.Run(GetAllUsers);
    }

    private async Task GetAllUsers()
    {
        try
        {
            Users.Clear();
            Users = new(await App.Database.GetUsers());
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w resjestrowaniu {ex.Message}", "no dobra");
        }
    }

    [RelayCommand]
    public async Task Edit(User user)
    {
        if (user.Email.Equals("admin")) return;

        IsBusy = true;
        var editUserPage = new EditUserPage(user);
        await Shell.Current.Navigation.PushAsync(editUserPage);
        IsBusy = false;
    }

    [RelayCommand]
    public async Task Delete(User user)
    {
        if (user.Email.Equals("admin")) return;

        IsBusy = true;
        try
        {
            await App.Database.DeleteUser(user);
            Users.Remove(user);
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"Blad przy usuwaniu: {user.Email} - {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task GoBack()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///Admin");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
