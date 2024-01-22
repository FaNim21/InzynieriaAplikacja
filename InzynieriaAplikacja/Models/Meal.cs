using SQLite;

namespace InzynieriaAplikacja.Models;

[Table("meal")]
public class Meal
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public float Bialko { get; set; }
    public float Tluszcze { get; set; }
    public float Weglowodany { get; set; }
    public float Kaloryczność { get; set; }
}
