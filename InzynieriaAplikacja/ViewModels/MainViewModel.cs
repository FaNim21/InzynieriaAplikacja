using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InzynieriaAplikacja.Models;
using Plugin.Maui.Pedometer;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace InzynieriaAplikacja.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private IPedometer pedometer;

    [ObservableProperty]
    private float step;

    [ObservableProperty]
    private int currentStep;

    [ObservableProperty]
    private int maxStep;


    public MainViewModel(IPedometer pedometer)
    {
        this.pedometer = pedometer;
        maxStep = App.CurrentUser.CelKrokow;
        StartCounting();
    }

    [RelayCommand]
    public async Task SignOut()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///Login");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("no i zepsoles", $"blad w wylogowywaniu {ex.Message}", "no dobra");
        }
        IsBusy = false;
    }

    [RelayCommand]
    public void MakeStep()
    {
        CurrentStep++;
        Step = (float) CurrentStep / MaxStep;
    }

    [RelayCommand]
    public async Task GoToTraining()
    {
        Trace.WriteLine(App.CurrentUser.Email);

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

    [RelayCommand]
    public async Task GoToUserProfile()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync("///UserProfileView");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
        IsBusy = false;
    }

    public void StartCounting()
    {
        pedometer.ReadingChanged += (sender, reading) =>
        {
            CurrentStep = reading.NumberOfSteps;
            Step = (float)CurrentStep / MaxStep;
        };
        try
        {
            pedometer.Start();
        }
        catch(Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("Error", ex.Message, ":(");
        }
    }
}
