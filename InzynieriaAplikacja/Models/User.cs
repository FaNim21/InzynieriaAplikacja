using SQLite;
using SQLiteNetExtensions.Attributes;

namespace InzynieriaAplikacja.Models;

[Table("user")]
public class User
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("administrator")]
    public bool Administrator { get; set; }

    [Column("imie")]
    public string Imie { get; set; }

    [Column("nazwisko")]
    public string Nazwisko { get; set; }

    [Column("e-mail")]
    public string Email { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("waga")]
    public float Waga { get; set; }

    [Column("wzrost")]
    public float Wzrost { get; set; }
}

public class TreningUzytkownik
{
    [ForeignKey(typeof(User))]
    [Column("id_uzytkownika")]
    public int IdUzytkownika { get; set; }

    [ForeignKey(typeof(Training))]
    [Column("id_treningu")]
    public int IdTreningu { get; set; }
}

public class Statystyki
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("spozyte_kalorie")]
    public float? SpozyteKalorie { get; set; }

    [Column("lista_spozytych_posilkow")]
    public string ListaSpozytychPosilkow { get; set; }

    [Column("wykonane_treningi")]
    public string WykonaneTreningi { get; set; }
}

public class StatystykiAktywnosci
{
    [Indexed]
    public int IdStatystyki { get; set; }

    [Indexed]
    public int IdAktywnosci { get; set; }
}
