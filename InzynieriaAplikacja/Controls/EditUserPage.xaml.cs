using InzynieriaAplikacja.Models;

namespace InzynieriaAplikacja.Controls;

public partial class EditUserPage : ContentPage
{
    private readonly User _user;

    public EditUserPage(User user)
    {
        InitializeComponent();
        _user = user;

        EmailEntry.Text = _user.Email;
        PasswordEntry.Text = _user.Password;
        ImieEntry.Text = _user.Imie;
        NazwiskoEntry.Text = _user.Nazwisko;
        WagaEntry.Text = _user.Waga.ToString();
        WzrostEntry.Text = _user.Wzrost.ToString();
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        _user.Email = EmailEntry.Text;
        _user.Password = PasswordEntry.Text;
        _user.Imie = ImieEntry.Text;
        _user.Nazwisko = NazwiskoEntry.Text;
        _user.Waga = float.Parse(WagaEntry.Text);
        _user.Wzrost = float.Parse(WzrostEntry.Text);

        App.Database.UpdateTable(_user);
        Navigation.PopAsync();
    }
}