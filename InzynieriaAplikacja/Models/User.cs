using SQLite;

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
