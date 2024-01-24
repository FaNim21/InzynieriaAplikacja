using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Controls;
using InzynieriaAplikacja.Models;
using System.Collections.ObjectModel;

namespace InzynieriaAplikacja.ViewModels.Administrator;

public partial class AdminControlMealViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Meal> meals = [];

    public AdminControlMealViewModel() { }

    public override void OnAppearing()
    {
        Task.Run(GetAllMeals);
    }

    private async Task GetAllMeals()
    {
        try
        {
            Meals.Clear();
            Meals = new(App.Database.GetTable<Meal>());
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w resjestrowaniu {ex.Message}", "no dobra");
        }
    }

    [RelayCommand]
    public async Task AddNewMeal()
    {
        try
        {
            Meal meal = new() { Name = "Posiłek", Kalorycznosc = 0f };
            Meals.Add(meal);
            App.Database.CreateTable(meal);
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("ERROR", $"blad przy dodawaniu treningu: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task Edit(Meal meal)
    {
        IsBusy = true;
        var editTrainingPage = new EditMealPage(meal);
        await Shell.Current.Navigation.PushAsync(editTrainingPage);
        IsBusy = false;
    }

    [RelayCommand]
    public async Task Delete(Meal meal)
    {
        IsBusy = true;
        try
        {
            App.Database.DeleteTable(meal);
            Meals.Remove(meal);
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"Blad przy usuwaniu: {meal.Name} - {ex.Message}", "no dobra");
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
