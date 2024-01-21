using SQLite;

namespace InzynieriaAplikacja.Models;

[Table("meal")]
public class Meal
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("bialko")]
    public float Bialko { get; set; }

    [Column("tluszcze")]
    public float Tluszcze { get; set; }

    [Column("weglowodany")]
    public float Weglowodany { get; set; }

    [Column("kalorycznosc")]
    public float Kaloryczność { get; set; }
}
