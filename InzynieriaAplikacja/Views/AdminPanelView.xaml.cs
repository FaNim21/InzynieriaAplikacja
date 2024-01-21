using InzynieriaAplikacja.ViewModels;
using InzynieriaAplikacja.ViewModels.Administrator;

namespace InzynieriaAplikacja.Views;

public partial class AdminPanelView : ContentPage
{
	public AdminPanelView(AdminPanelViewModel viewModel)
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