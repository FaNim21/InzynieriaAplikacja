using SQLite;

namespace InzynieriaAplikacja.Models;

[Table("training")]
public class Training
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("time")]
    public string Time { get; set; }

    [Column("spalone_kalorie")]
    public float SpaloneKalorie { get; set; }

    [Column("cwiczenia")]
    public string Cwiczenia { get; set; }
}
