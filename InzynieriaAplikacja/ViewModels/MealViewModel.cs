using CommunityToolkit.Mvvm.ComponentModel;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels;

public partial class MealViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Meal> meals = [];


    public MealViewModel() { }
    public override void OnAppearing()
    {
        Task.Run(GetMeals);
    }

    public async Task GetMeals()
    {
        try
        {
            Meals.Clear();
            Meals = new(App.Database.GetTable<Meal>());
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("ERROR", $"Błąd przy pobieraniu posiłków: {ex.Message}", "OK");
        }
    }

}
