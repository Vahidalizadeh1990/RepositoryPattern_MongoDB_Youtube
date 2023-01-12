using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MainApp.Models;

public class Music
{
    [BsonRepresentation(BsonType.String)]
    [BsonElement("id")]
    public string Id { get; set; }
    [BsonElement("title")]
    [Required]
    public string Title { get; set; }
    [BsonElement("releaseDate")]
    public DateTime ReleaseDate { get; set; }
    [BsonElement("artist")]
    public string Artist { get; set; }
    [BsonElement("rate")]
    public int Rate { get; set; }
}