using InzynieriaAplikacja.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

namespace InzynieriaAplikacja.Context;

public class LocalDatabaseService
{
    private readonly SQLiteConnection _connection;


    public LocalDatabaseService()
    {
        if (_connection is not null) return;
        _connection = new SQLiteConnection(AppConfig.DatabasePath, AppConfig.Flags);
        _connection.CreateTable<User>();
        _connection.CreateTable<Activity>();
        _connection.CreateTable<Statistic>();
        _connection.CreateTable<Training>();
        _connection.CreateTable<Meal>();

        //_connection.CreateTable<EatenMeal>();
    }

    public void DropTable<T>() where T : new()
    {
        _connection.DropTable<T>();
    }
    public List<T> GetTable<T>() where T : new()
    {
        return _connection.Table<T>().ToList();
    }
    public void CreateTable<T>(T table) where T : new()
    {
        _connection.InsertWithChildren(table, recursive: true);
    }
    public void UpdateTable<T>(T table) where T : new()
    {
        _connection.UpdateWithChildren(table);
    }
    public void DeleteTable<T>(T table) where T : new()
    {
        _connection.Delete(table);
    }

    #region Users
    public User GetUserById(int id)
    {
        return _connection.GetWithChildren<User>(id, recursive: true);
    }
    public User? GetUserByEmail(string email)
    {
        User user = _connection.Table<User>().Where(x => x.Email == email).FirstOrDefault();
        if (user != null) return _connection.GetWithChildren<User>(user.Id);
        return null;
    }
    public User LoginUser(string email, string password)
    {
        User? user = GetUserByEmail(email) ?? throw new Exception($"Nie istnieje użytkownik o e-mailu: {email}");
        if (user.Password != password) throw new Exception("Nie prawidłowe hasło!");

        return _connection.GetWithChildren<User>(user.Id);
    }
    public void CreateUser(User user)
    {
        var foundUser = GetUserByEmail(user.Email);
        if (foundUser != null)
            throw new Exception($"Użytkownik o tym mailu {user.Email} juz istnieje");

        _connection.InsertWithChildren(user);
    }
    #endregion

    #region Trainings
    public Training GetTrainingById(int id)
    {
        return _connection.GetWithChildren<Training>(id, recursive: true);
    }
    #endregion

    /*#region Statistics

    #endregion

    #region Activities

    #endregion*/
}
