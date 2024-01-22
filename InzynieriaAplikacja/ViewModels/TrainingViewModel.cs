using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Controls;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels;

public partial class TrainingViewModel: BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Training>? trainings;


    public TrainingViewModel()
    {
        
    }
    
    //TODO: Zrobic metode do pobierania treningow z bazy danych
    public async Task GetTrainings()
    {

    }

    [RelayCommand]
    public async Task GoBack()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///Main");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }
}
