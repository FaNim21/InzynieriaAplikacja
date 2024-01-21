using InzynieriaAplikacja.ViewModels;

namespace InzynieriaAplikacja.Views;

public partial class TrainingView : ContentPage
{
	public TrainingView(TrainingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}