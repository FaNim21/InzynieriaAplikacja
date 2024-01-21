using MongoDB.Bson;
using Realms;

namespace InzynieriaAplikacja.Models;

public class Training : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [MapTo("_name")]
    public string Name { get; set; }

    [MapTo("_time")]
    public string Time { get; set; }

    [MapTo("_spalone_kalorie")]
    public float SpaloneKalorie { get; set; }

    [MapTo("_czwiecznia")]
    public string Cwiczenia { get; set; }
}
