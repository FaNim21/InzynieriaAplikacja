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

    private static Statistic _currentStatistics;
    public static Statistic CurrentStatistics
    {
        get => _currentStatistics;
        set { _currentStatistics = value; }
    }

    private static Activity _currentActivity;
    public static Activity CurrentActivity
    {
        get => _currentActivity;
        set { _currentActivity = value; }
    }

    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
