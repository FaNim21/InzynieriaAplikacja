using InzynieriaAplikacja.Views;
using InzynieriaAplikacja.Views.AdminViews;

namespace InzynieriaAplikacja
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("///Login");
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }
}
