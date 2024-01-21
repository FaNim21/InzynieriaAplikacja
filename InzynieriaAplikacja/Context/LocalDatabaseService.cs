using InzynieriaAplikacja.Models;
using SQLite;

namespace InzynieriaAplikacja.Context;

public class LocalDatabaseService
{
    private readonly SQLiteAsyncConnection _connection;


    public LocalDatabaseService()
    {
        if (_connection is not null) return;

        _connection = new SQLiteAsyncConnection(AppConfig.DatabasePath, AppConfig.Flags);
        _connection.CreateTableAsync<User>();
        _connection.CreateTableAsync<Training>();
        _connection.CreateTableAsync<Meal>();
    }

    #region Users
    public async Task<List<User>> GetUsers()
    {
        return await _connection.Table<User>().ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _connection.Table<User>().Where(x => x.Id == id).FirstAsync();
    }

    public async Task<User> GetUsersByEmail(string email)
    {
        return await _connection.Table<User>().Where(x => x.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> LoginUser(string email, string password)
    {
        return await _connection.Table<User>().Where(user => user.Email == email && user.Password == password).FirstAsync();
    }

    public async Task CreateUser(User user)
    {
        var foundUser = await GetUsersByEmail(user.Email);
        if (foundUser != null)
            throw new Exception($"Użytkownik o tym mailu {user.Email} juz istnieje");

        await _connection.InsertAsync(user);
    }

    public async Task UpdateUser(User user)
    {
        await _connection.UpdateAsync(user);
    }

    public async Task DeleteUser(User user)
    {
        await _connection.DeleteAsync(user);
    }
    #endregion

    /*#region Trainings
    public async Task<List<Training>> GetTrainings()
    {
        return await _connection.Table<Training>().ToListAsync();
    }

    public async Task<Training> GetTrainingById(int id)
    {
        return await _connection.Table<Training>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateTraining(Training user)
    {
        await _connection.InsertAsync(user);
    }

    public async Task UpdateTraining(Training user)
    {
        await _connection.UpdateAsync(user);
    }

    public async Task DeleteTraining(Training user)
    {
        await _connection.DeleteAsync(user);
    }
    #endregion*/
}
