using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;

namespace InzynieriaAplikacja.ViewModels;

public partial class UserAddMealViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? name;
    [ObservableProperty]
    private float kalorycznosc;
    [ObservableProperty]
    private float bialko;
    [ObservableProperty]
    private float tluszcze;
    [ObservableProperty]
    private float weglowodany;


    public UserAddMealViewModel() { }

    [RelayCommand]
    public async Task AddNewMeal()
    {
        try
        {
            Meal meal = new()
            {
                Name = Name!,
                Kalorycznosc = Kalorycznosc,
                Bialko = Bialko,
                Tluszcze = Tluszcze,
                Weglowodany = Weglowodany
            };
            App.Database.CreateTable(meal);

            await Application.Current!.MainPage!.DisplayAlert("Error", $"Dodano posiłek: {Name}", "OK");
            Clear();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", $"Błąd w dodawaniu posiłu: {ex.Message}", "OK");
        }
    }

    private void Clear()
    {
        Name = string.Empty;
        Kalorycznosc = 0;
        Bialko = 0;
        Tluszcze = 0;
        Weglowodany = 0;
    }
}
