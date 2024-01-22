using InzynieriaAplikacja.ViewModels;

namespace InzynieriaAplikacja.Views;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ((BaseViewModel)BindingContext).OnAppearing();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ((BaseViewModel)BindingContext).OnDisappearing();
    }
}