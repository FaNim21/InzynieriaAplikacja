using InzynieriaAplikacja.ViewModels;

namespace InzynieriaAplikacja.Views;

public partial class UserAddMealView : ContentPage
{
	public UserAddMealView(UserAddMealViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}