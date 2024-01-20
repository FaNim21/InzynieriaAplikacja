namespace InzynieriaAplikacja;

public partial class App : Application
{
    public static Realms.Sync.App RealmApp;

    public App()
    {
        InitializeComponent();

        RealmApp = Realms.Sync.App.Create(AppConfig.AppID);

        MainPage = new AppShell();
    }
}
