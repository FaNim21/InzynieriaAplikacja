using SQLite;
using SQLiteNetExtensions.Attributes;

namespace InzynieriaAplikacja.Models;

[Table("user")]
public class User
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public bool Administrator { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public float Waga { get; set; }
    public float Wzrost { get; set; }
    public int CelKrokow { get; set; }


    [ForeignKey(typeof(Statistic))]
    public int IdStatistic { get; set; }

    [OneToOne(CascadeOperations = CascadeOperation.All)]
    public Statistic? Statistic { get; set; } = new();
}

[Table("statistic")]
public class Statistic
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public float? SpozyteKalorie { get; set; }
    public string? WykonaneTreningi { get; set; }


    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<Meal> EatenMeals { get; set; } = [];


    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<Training> Trainings { get; set; } = [];


    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<Activity> Activities { get; set; } = [];
}

/*[Table("zjedzony_posilek")]
public class EatenMeal
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }


    [ForeignKey(typeof(User))]
    public int IdUser { get; set; }

    [OneToOne(CascadeOperations = CascadeOperation.All)]
    public User User { get; set; } = new();


    [ForeignKey(typeof(Meal))]
    public int IdMeal { get; set; }

    [OneToOne(CascadeOperations = CascadeOperation.All)]
    public Meal? Meal { get; set; } = new();
}*/

[Table("activity")]
public class Activity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime? Data { get; set; }
    public int? Kroki { get; set; }
    public float SpaloneKalorie { get; set; }
    public float PokonanyDystans { get; set; }
    public string? AktywnosciDodatkowe { get; set; }

    [ForeignKey(typeof(Statistic))]
    public int StatisticId { get; set; }
}
