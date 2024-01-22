using SQLite;

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
