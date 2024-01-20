using InzynieriaAplikacja.ViewModels;

namespace InzynieriaAplikacja.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}