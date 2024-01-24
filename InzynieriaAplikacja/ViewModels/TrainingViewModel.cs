using CommunityToolkit.Mvvm.ComponentModel;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels;

public partial class TrainingViewModel: BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Training> trainings = [];


    public TrainingViewModel() { }

    public override void OnAppearing()
    {
        Task.Run(GetTrainings);
    }

    public async Task GetTrainings()
    {
        try
        {
            Trainings.Clear();
            Trainings = new(App.Database.GetTable<Training>());
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("ERROR", $"Błąd przy pobieraniu treningów: {ex.Message}", "OK");
        }
    }
}
