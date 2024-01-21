using InzynieriaAplikacja.Views;
using InzynieriaAplikacja.Views.AdminViews;

namespace InzynieriaAplikacja
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(MainView), typeof(MainView));
            Routing.RegisterRoute(nameof(AdminPanelView), typeof(AdminPanelView));
            Routing.RegisterRoute(nameof(AdminControlUserView), typeof(AdminControlUserView));
        }
    }
}
