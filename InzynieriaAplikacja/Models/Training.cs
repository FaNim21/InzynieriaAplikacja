using SQLite;
using SQLiteNetExtensions.Attributes;

namespace InzynieriaAplikacja.Models;

[Table("training")]
public class Training
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Time { get; set; }
    public float SpaloneKalorie { get; set; }
    public string Cwiczenia { get; set; }
}

[Table("skonczony_trening")]
public class FinishedTraining
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }


    [ForeignKey(typeof(User))]
    public int IdUser { get; set; }

    [OneToOne(CascadeOperations = CascadeOperation.All)]
    public User User { get; set; } = new();


    [ForeignKey(typeof(Training))]
    public int IdTraining { get; set; }

    [OneToOne(CascadeOperations = CascadeOperation.All)]
    public Training? Training { get; set; } = new();
}
