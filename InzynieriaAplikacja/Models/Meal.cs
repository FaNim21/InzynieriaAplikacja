using SQLite;
using SQLiteNetExtensions.Attributes;

namespace InzynieriaAplikacja.Models;

[Table("meal")]
public class Meal
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public float Kalorycznosc { get; set; }
    public float Bialko { get; set; }
    public float Tluszcze { get; set; }
    public float Weglowodany { get; set; }
}

[Table("zjedzony_posilek")]
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
}
