using Realms;

namespace InzynieriaAplikacja.Models;

public class Training : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public int Id { get; set; }
}
