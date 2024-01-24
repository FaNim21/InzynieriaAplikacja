using InzynieriaAplikacja.ViewModels;
using InzynieriaAplikacja.ViewModels.Administrator;

namespace InzynieriaAplikacja.Views.AdminViews;

public partial class AdminControlMealView : ContentPage
{
	public AdminControlMealView(AdminControlMealViewModel viewModel)
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