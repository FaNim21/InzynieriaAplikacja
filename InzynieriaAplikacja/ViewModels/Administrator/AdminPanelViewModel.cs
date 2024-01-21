using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels.Administrator;

public partial class AdminPanelViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<User> users = [];


    public AdminPanelViewModel()
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
        IsBusy = true;

        /*string action = await Application.Current!.MainPage!.DisplayActionSheet("Edit User", "Cancel", null, "Edit Email", "Edit Password", "Edit Other Details");

        if (action == "Edit Email")
        {
            // Handle email editing
        }
        else if (action == "Edit Password")
        {
            // Handle password editing
        }
        else if (action == "Edit Other Details")
        {
            // Handle editing other details
        }*/

        //await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w resjestrowaniu {user.Email}", "no dobra");
        IsBusy = false;
    }

    [RelayCommand]
    public async Task Delete(User user)
    {
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
    public async Task SignOut()
    {
        IsBusy = true;
        try
        {
            //await App.Database.
            await Shell.Current.GoToAsync("///Login");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
