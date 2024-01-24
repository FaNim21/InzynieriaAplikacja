using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels;

public partial class TrainingDoneViewModel: BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<FinishedTraining> trainingsDone = [];

    public TrainingDoneViewModel()
    {
        
    }

    public override void OnAppearing()
    {
        Task.Run(GetTrainings);
    }

    public async Task GetTrainings()
    {
        try
        {
            TrainingsDone.Clear();
            TrainingsDone = new(App.Database.GetTable<FinishedTraining>());
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("ERROR", $"Błąd przy pobieraniu treningów: {ex.Message}", "OK");
        }
    }
}
