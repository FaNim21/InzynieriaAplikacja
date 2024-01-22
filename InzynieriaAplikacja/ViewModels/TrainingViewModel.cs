using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Controls;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels;

public partial class TrainingViewModel: BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Training> trainings = [];


    public TrainingViewModel()
    {
        
    }

    public override void OnAppearing()
    {
        Task.Run(GetTrainings);
    }

    //TODO: Zrobic metode do pobierania treningow z bazy danych
    public async Task GetTrainings()
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
}
