using CommunityToolkit.Mvvm.Input;

namespace InzynieriaAplikacja.ViewModels;

public partial class TrainingViewModel: BaseViewModel
{

    public TrainingViewModel()
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
