using InzynieriaAplikacja.Context;
using InzynieriaAplikacja.Models;

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

    private static User _currentUser;
    public static User CurrentUser
    {
        get { return _currentUser; }
        set
        {
            _currentUser = value;
        }
    }

    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
