﻿using CommunityToolkit.Mvvm.ComponentModel;
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

    public override void OnAppearing()
    {
        base.OnAppearing();

        MaxStep = App.CurrentUser.CelKrokow;
    }

    [RelayCommand]
    public void MakeStep()
    {
        CurrentStep++;
        Step = (float) CurrentStep / MaxStep;
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
