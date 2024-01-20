using InzynieriaAplikacja.Views;

namespace InzynieriaAplikacja
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(MainView), typeof(MainView));
        }
    }
}
