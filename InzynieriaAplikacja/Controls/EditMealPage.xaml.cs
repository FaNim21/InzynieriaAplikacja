using InzynieriaAplikacja.Models;

namespace InzynieriaAplikacja.Controls;

public partial class EditMealPage : ContentPage
{
    private readonly Meal _meal;

    public EditMealPage(Meal meal)
    {
        InitializeComponent();
        _meal = meal;

        NameEntry.Text = _meal.Name;
        CaloriesEntry.Text = _meal.Kalorycznosc.ToString();
        BialoEntry.Text = _meal.Bialko.ToString();
        TluszczeEntry.Text = _meal.Tluszcze.ToString();
        WegloEntry.Text = _meal.Weglowodany.ToString();
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        _meal.Name = NameEntry.Text;
        _meal.Kalorycznosc = float.Parse(CaloriesEntry.Text);
        _meal.Bialko = float.Parse(BialoEntry.Text);
        _meal.Tluszcze = float.Parse(TluszczeEntry.Text);
        _meal.Weglowodany = float.Parse(WegloEntry.Text);

        App.Database.UpdateTable(_meal);
        Navigation.PopAsync();
    }
}