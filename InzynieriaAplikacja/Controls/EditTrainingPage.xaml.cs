using InzynieriaAplikacja.Models;

namespace InzynieriaAplikacja.Controls;

public partial class EditTrainingPage : ContentPage
{
    private readonly Training _training;

	public EditTrainingPage(Training training)
	{
        InitializeComponent();
        _training = training;

        NameEntry.Text = _training.Name;
        DescriptionEntry.Text = _training.Description;
        TimeEntry.Text = _training.Time;
        CaloriesEntry.Text = _training.SpaloneKalorie.ToString();
        TrainingEntry.Text = _training.Cwiczenia;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        _training.Name = NameEntry.Text;
        _training.Description = DescriptionEntry.Text;
        _training.Time = TimeEntry.Text;
        _training.SpaloneKalorie = float.Parse(CaloriesEntry.Text);
        _training.Cwiczenia= TrainingEntry.Text;

        App.Database.UpdateTable(_training);
        Navigation.PopAsync();
    }
}