using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels.Administrator;

public partial class AdminControlTrainingViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Training> trainings = [];

    public AdminControlTrainingViewModel() { }

    public override void OnAppearing()
    {
        Task.Run(GetAllTrainings);
    }

    private async Task GetAllTrainings()
    {
        try
        {
            Trainings.Clear();
            Trainings = new(App.Database.GetTable<Training>());
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w resjestrowaniu {ex.Message}", "no dobra");
        }
    }

    [RelayCommand]
    public async Task AddNewTraining()
    {
        try
        {
            Training training = new() { Name = "Trening", Description = "Do trenowania"};
            Trainings.Add(training);
            App.Database.CreateTable(training);
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("ERROR", $"blad przy dodawaniu treningu: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task Edit(Training training)
    {
        IsBusy = true;
        /*var editUserPage = new EditUserPage(user);
        await Shell.Current.Navigation.PushAsync(editUserPage);*/
        IsBusy = false;
    }

    [RelayCommand]
    public async Task Delete(Training training)
    {
        IsBusy = true;
        try
        {
            App.Database.DeleteTable(training);
            Trainings.Remove(training);
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"Blad przy usuwaniu: {training.Name} - {ex.Message}", "no dobra");
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
