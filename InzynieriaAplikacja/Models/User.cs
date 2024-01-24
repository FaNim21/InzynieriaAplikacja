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
    public int RokUrodzenia { get; set; }
    public int CelKrokow { get; set; }


    [ForeignKey(typeof(Statistic))]
    public int IdStatistic { get; set; }
}

[Table("statistic")]
public class Statistic
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int WykonaneKroki { get; set; }
    public float CalkowiciePrzebytyDystans { get; set; }
    public int LaczneSpaloneKalorie { get; set; }
    public float SpozyteKalorie { get; set; }
    public string? WykonaneTreningi { get; set; }
}

[Table("activity")]
public class Activity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    //DateTime asdasd = Convert.ToDateTime(today); zamiana daty na string
    public DateTime Date { get; set; }
    public int Kroki { get; set; }
    public int SpaloneKalorie { get; set; }
    public float PokonanyDystans { get; set; }
    public string? AktywnosciDodatkowe { get; set; }

    [ForeignKey(typeof(Statistic))]
    public int StatisticId { get; set; }
}
