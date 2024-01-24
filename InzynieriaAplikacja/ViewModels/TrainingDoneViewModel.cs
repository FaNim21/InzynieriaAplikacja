using CommunityToolkit.Mvvm.ComponentModel;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels;

public partial class TrainingDoneViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Training> trainingsDone = [];

    public TrainingDoneViewModel() { }

    public override void OnAppearing()
    {
        Task.Run(GetTrainings);
    }

    public async Task GetTrainings()
    {
        try
        {
            TrainingsDone.Clear();

            var finishedTrainings = App.Database.GetTable<FinishedTraining>().Where(x => x.IdUser == App.CurrentUser.Id);
            foreach (var finishedTraining in finishedTrainings)
            {
                Training? training = App.Database.GetTable<Training>().Where(x => x.Id == finishedTraining.IdTraining).FirstOrDefault();
                if (training == null) continue;
                TrainingsDone.Add(training);
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("ERROR", $"Błąd przy pobieraniu treningów: {ex.Message}", "OK");
        }
    }
}
