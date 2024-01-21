using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Training>? trainings;

    [ObservableProperty]
    private float step;

    [ObservableProperty]
    private int currentStep;

    private int maxStep=100;


    public MainViewModel()
    {
        Trainings = [];
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

    [RelayCommand]
    public void AddTraining()
    {
        IsBusy = true;

        var todo = new Training
        {
            Name = "asdasd123123213",
            Time = DateTime.UtcNow.ToString(),
            SpaloneKalorie = 1000,
            Cwiczenia = "eloo"
        };
        Trainings!.Add(todo);

        IsBusy = false;
    }

    [RelayCommand]
    public void DeleteTraining()
    {
        if (Trainings == null) return;
        IsBusy = true;

        Training lastTraining = Trainings.LastOrDefault()!;
        Trainings.Remove(lastTraining);

        IsBusy = false;
    }

    [RelayCommand]
    public void MakeStep()
    {
        CurrentStep++;
        Step = (float) CurrentStep / maxStep;
    }

    [RelayCommand]
    public async Task GoToTraining()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///Training");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
        IsBusy = false;
    }
}
