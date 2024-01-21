using InzynieriaAplikacja.Context;

namespace InzynieriaAplikacja;

public partial class App : Application
{
    private static LocalDatabaseService? _database;
    public static LocalDatabaseService Database
    {
        get
        {
            _database ??= new();
            return _database;
        }
    }


    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
