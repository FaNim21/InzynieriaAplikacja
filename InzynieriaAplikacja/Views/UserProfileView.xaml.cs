using InzynieriaAplikacja.ViewModels;

namespace InzynieriaAplikacja.Views;

public partial class UserProfileView : ContentPage
{
	public UserProfileView(UserProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}